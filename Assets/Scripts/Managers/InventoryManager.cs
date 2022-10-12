/*  Filename:           InventoryManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Stores player inventory on hand; contains functions to add, reduce and clear items on hand
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private Dictionary<GameEnums.ItemType, int> inventoryDictionary = new Dictionary<GameEnums.ItemType, int>();
    public Dictionary<GameEnums.ItemType, int> InventoryDictionary { get { return inventoryDictionary; } }

    public bool Inited { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ClearInventory()
    {
        inventoryDictionary.Clear();
    }

    public void AddItem(GameEnums.ItemType itemType, int count)
    {
        if (inventoryDictionary.ContainsKey(itemType))
        {
            inventoryDictionary[itemType] += count;            
        }
        else
        {
            inventoryDictionary.Add(itemType, count);
        }

        if (NotificationController.Instance != null
                && ItemManager.Instance != null
                && ItemManager.Instance.Inited)
        {
            ItemData itemData = ItemManager.Instance.GetItemData(itemType);
            if (itemData != null)
            {
                NotificationController.Instance.Notify(itemData.icon, itemData.name, count);
            }
        }
    }

    public bool ReduceItem(GameEnums.ItemType itemType, int count)
    {
        if (inventoryDictionary.ContainsKey(itemType) && inventoryDictionary[itemType] >= count)
        {
            inventoryDictionary[itemType] -= count;
            return true;
        }

        return false;
    }
}
