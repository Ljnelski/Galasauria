/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 2, 2022
 *  Description:        Creates and Deletes Weapons, and Acts as a wrapper to the IEquipable Equiped Item
 *  Revision History:   November 2, 2022 (Liam Nelski): Initial script. *                     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItem : MonoBehaviour
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

    public bool WeaponEquiped
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
    public void LoadWeapon(GameObject weaponPrefab)
    {
        Destroy(currentObject);
        currentObject = Instantiate(weaponPrefab, transform);
        equipedItem = currentObject.GetComponent<IEquipable>();
    }

    public void UseItem(GameEnums.EquipableInput inputValue)
    {
        equipedItem.BeginUse(inputValue);
    }

}
