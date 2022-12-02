/*  Filename:           EquipSlot.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 2, 2022
 *  Description:        Creates and Deletes Weapons, and Acts as a wrapper to the IEquipable Equiped Item
 *  Revision History:   November 2, 2022 (Liam Nelski): Initial script.
 *                      November 4, 2022 (Liam Nelski): Renamed To EquipSlot 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EquipSlot : MonoBehaviour
{
    [SerializeField] private Transform _leftHandIKTarget;
    [SerializeField] private Transform _rightHandIKTarget;

    public bool InUse { 
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
        if(_equipedItem != null )
        {
            _leftHandIKTarget.transform.SetPositionAndRotation(_equipedItem.LeftHandIKTransform.position, _equipedItem.LeftHandIKTransform.rotation);
            _rightHandIKTarget.transform.SetPositionAndRotation(_equipedItem.RightHandIKTransform.position, _equipedItem.RightHandIKTransform.rotation);
        }
    }

    public void LoadWeapon(GameObject weaponPrefab)
    {
        if(_equipedItem != null)        
            Destroy(_equipedItem.gameObject);
        
        _equipedItem = Instantiate(weaponPrefab, transform).GetComponent<EquipableItem>();
    }

    public void UseItem(GameEnums.EquipableInput inputValue)
    {
        _equipedItem.BeginUse(inputValue);
    }

}
