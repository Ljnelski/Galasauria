/*  Filename:           InventoryScreen.cs
 *  Author:             Yuk Yee Wong (301234795), Liam Nelski (301064116)
 *  Last Update:        November 26, 2022
 *  Description:        Displays inventory on hand using data from inventory manager and item manager
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 13, 2022 (Liam Nelski): Removed Dependency on InventoryManager
 *                      November 26, 2022 (Yuk Yee Wong): Fixed null reference exception of _targetScript; inherited UIPlayerDataReaderScreen to get time freezed when OnEnable and unfreezed when OnDisable; fixed a bug in clearing the grid.
 */

using System.Collections;
using UnityEngine;

public class InventoryScreen : UIPlayerDataReaderScreen<Inventory>
{
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private InventoryItemDisplay inventoryItemPrefab;
    [SerializeField] private InventoryItemDetailsDisplay inventoryItemDetailsDisplay;

    protected override void OnEnable()
    {
        base.OnEnable();

        inventoryItemDetailsDisplay.RefreshInventoryScreen += Refresh;

        Refresh();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        inventoryItemDetailsDisplay.RefreshInventoryScreen -= Refresh;
    }

    private void Refresh()
    {
        // clear the grid
        if (inventoryContainer.childCount > 0)
        {
            for (int i = inventoryContainer.childCount - 1; i > -1; i--)
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

        inventoryItemDetailsDisplay.ResetDisplay();
    }
}
    
