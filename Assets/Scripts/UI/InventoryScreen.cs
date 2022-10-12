/*  Filename:           InventoryScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Displays inventory on hand using data from inventory manager and item manager
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using UnityEngine;

public class InventoryScreen : MonoBehaviour
{
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private InventoryItemDisplay inventoryItemPrefab;

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
        
        StartCoroutine(waitForManager());
    }

    private IEnumerator waitForManager()
    {
        if (InventoryManager.Instance == null
            || ItemManager.Instance == null || !ItemManager.Instance.Inited)
        {
            yield return new WaitForEndOfFrame();
        }

        // add item to the grid
        foreach (var pair in InventoryManager.Instance.InventoryDictionary)
        {
            InventoryItemDisplay display = Instantiate(inventoryItemPrefab, inventoryContainer);
            ItemData itemData = ItemManager.Instance.GetItemData(pair.Key);
            if (itemData != null)
            {
                display.Init(itemData.icon, pair.Value, itemData.name);
            }
        }
    }

}
