/*  Filename:           InventoryScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Displays inventory on hand using data from inventory manager and item manager
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 13 2022 (Liam Nelski): Removed Dependency on InventoryManager
 */

using System.Collections;
using UnityEngine;

public class InventoryScreen : UIPlayerDataReader<Inventory>
{
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private InventoryItemDisplay inventoryItemPrefab;
    [SerializeField] private InventoryItemDetailsDisplay inventoryItemDetailsDisplay;

    private void Start()
    {
        GetTargetScript();
    }

    private void OnEnable()
    {
        // clear the grid
        if (inventoryContainer.childCount > 0)
        {
            for (int i = inventoryContainer.childCount - 1; i > 0; i--)
            {
                // TODO, recycle
                Destroy(inventoryContainer.GetChild(i).gameObject);
            }
        }

        // add item to the grid
        foreach (Item item in _targetScript.inventory)
        {
            InventoryItemDisplay display = Instantiate(inventoryItemPrefab, inventoryContainer);
            ItemData itemData = item.data;
            if (itemData != null)
            {
                display.Init(itemData, item.stackSize);
                display.OnItemClick = null;
                display.OnItemClick += inventoryItemDetailsDisplay.Display;
            }
        }
    }
}
    
