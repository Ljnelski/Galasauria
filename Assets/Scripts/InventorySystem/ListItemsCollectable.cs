/*  Filename:           ListItemsCollectable.cs
 *  Author:             Yuk Yee Wong (301234795), 
 *                      Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Player can pick up item and add to inventory when collide with the trigger
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      October 14 2022 (Liam Nelski): Changed to use ICollectable interface and renamed file and class
 */

using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ListItemsCollectable : MonoBehaviour, ICollectable
{
    [SerializeField]
    private List<ItemAmount> itemCountData;

    public void Collect(Inventory inventory)
    {
        foreach (ItemAmount data in itemCountData)
        {
            for (int i = 0; i < data.amount; i++)
            {
                inventory.AddItem(data.itemData);

            }
        }

        // TODO, recycle
        Destroy(gameObject);
    }
}

[Serializable]
public struct ItemAmount
{
    [SerializeField]
    public ItemData itemData;
    [SerializeField]
    public float amount;
   
}
