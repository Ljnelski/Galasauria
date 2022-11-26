/*  Filename:           EquipSlot.cs
 *  Author:             Liam Nelski (301064116), Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        Creates and Deletes Weapons, and Acts as a wrapper to the IEquipable Equiped Item
 *  Revision History:   November 2, 2022 (Liam Nelski): Initial script.
 *                      November 4, 2022 (Liam Nelski): Renamed To EquipSlot 
 *                      November 25, 2022 (Yuk Yee Wong): Added an action argument in LoadWeapons and passed the argument to destroyer
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : MonoBehaviour
{
    GameObject currentObject;
    public bool InUse { 
        get
        {
            if (equipedItem == null || !equipedItem.InUse)
                return false;
            return true;
        } 
    }

    public bool ItemEquiped
    {
        get
        {
            return equipedItem != null;
        }
    }

    private IEquipable equipedItem;

    private void Awake()
    {
        equipedItem = GetComponentInChildren<IEquipable>();
    }
    public void LoadWeapon(GameObject weaponPrefab, Action<int> onDestoryEnemyScore)
    {
        Destroy(currentObject);
        currentObject = Instantiate(weaponPrefab, transform);

        Destroyer destroyer = currentObject.GetComponentInChildren<Destroyer>();
        if (destroyer != null)
        {
            destroyer.OnTargetDestroyedAction += onDestoryEnemyScore;
        }

        equipedItem = currentObject.GetComponent<IEquipable>();
    }

    public void UseItem(GameEnums.EquipableInput inputValue)
    {
        equipedItem.BeginUse(inputValue);
    }

}
