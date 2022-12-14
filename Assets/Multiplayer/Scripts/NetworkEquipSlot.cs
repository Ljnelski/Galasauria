/*  Filename:           NetworkEquipSlot.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkEquipSlot : NetworkBehaviour
{
    [SerializeField] private List<GameObject> _equipables;
    [SerializeField] private Transform _leftHandIKTarget;
    [SerializeField] private Transform _rightHandIKTarget;

    private int equipablesIndex = -1;
    private Action<int> tempOnDestroyEnemyScore;
    private Inventory tempItemOwnerInventory;
    private NetworkVariable<int> networkEquipableIndex = new NetworkVariable<int>();

    public bool InUse
    {
        get
        {
            if (_equipedItem == null || !_equipedItem.InUse)
                return false;
            return true;
        }
    }

    public bool ItemEquiped
    {
        get
        {
            return _equipedItem != null;
        }
    }

    private EquipableItem _equipedItem;

    private void Awake()
    {
        _equipedItem = GetComponentInChildren<EquipableItem>();
    }

    private void Update()
    {
        if (_equipedItem != null)
        {
            _leftHandIKTarget.transform.SetPositionAndRotation(_equipedItem.LeftHandIKTransform.position, _equipedItem.LeftHandIKTransform.rotation);
            _rightHandIKTarget.transform.SetPositionAndRotation(_equipedItem.RightHandIKTransform.position, _equipedItem.RightHandIKTransform.rotation);
        }

        if (IsClient)
        {
            if (!equipablesIndex.Equals(networkEquipableIndex.Value))
            {
                equipablesIndex = networkEquipableIndex.Value;
                UpdateWeapon();
            }
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsClient & IsOwner)
        {
            UpdateWeaponTypeServerRpc(-1);
        }
    }
    
    private void UpdateWeapon()
    {
        // Destroy Previous Item
        if (_equipedItem != null)
        {
            Destroy(_equipedItem.gameObject);
        }

        // Create new Item
        if (networkEquipableIndex.Value >= 0 && _equipables.Count > networkEquipableIndex.Value)
        {
            _equipedItem = Instantiate(_equipables[networkEquipableIndex.Value], transform).GetComponent<EquipableItem>();
        }

        if (_equipedItem != null)
        {
            // Link Destroyer
            Destroyer destroyer = _equipedItem.GetComponentInChildren<Destroyer>();
            if (destroyer != null && tempOnDestroyEnemyScore != null)
            {
                destroyer.OnTargetDestroyedAction += tempOnDestroyEnemyScore;
            }

            // inject reference to inventory
            if (tempItemOwnerInventory != null)
            {
                _equipedItem.ItemOwnerInventory = tempItemOwnerInventory;
            }
        }
    }

    public void LoadWeapon(Action<int> onDestroyEnemyScore, Inventory itemOwnerInventory)
    {
        int newEquipablesIndex = -1;

        if (_equipables.Count == 0)
        {
            newEquipablesIndex = -1;
        }
        else if (_equipables.Count == 1)
        {
            newEquipablesIndex = 0;
        }
        else
        {
            newEquipablesIndex = (equipablesIndex + 1) % Mathf.Max(_equipables.Count, 0);
        }

        // No weapon avalible, Clear Weapon (Should Not Happen In Gameplay)
        if (newEquipablesIndex == -1)
        {
            tempOnDestroyEnemyScore = null;
            tempItemOwnerInventory = null;
        }
        else
        {
            tempOnDestroyEnemyScore = onDestroyEnemyScore;
            tempItemOwnerInventory = itemOwnerInventory;
        }

        UpdateWeaponTypeServerRpc(newEquipablesIndex);
    }

    [ServerRpc]
    private void UpdateWeaponTypeServerRpc(int newEquipablesIndex)
    {
        networkEquipableIndex.Value = newEquipablesIndex;
    }

    public void UseItem(GameEnums.EquipableInput inputValue)
    {
        _equipedItem.BeginUse(inputValue);
    }

}
