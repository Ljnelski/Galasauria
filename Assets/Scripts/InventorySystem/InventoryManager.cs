/*  Filename:           InventoryManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Stores player inventory on hand; contains functions to add, reduce and clear items on hand
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      Octover 14, 2022 (Liam Nelski): Reworked into global inventory manager
 */

using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Dictionary<int, Inventory> Inventories;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Inventories = new Dictionary<int, Inventory>();
    }

    public void AssignInventory(int inventoryId, Inventory newInventory)
    {
        bool inventoryExists = Inventories.TryGetValue(inventoryId, out Inventory duplicateInventory);
        Debug.Log(inventoryExists);
        if (inventoryExists)
        {
            Debug.LogError("InventoryManager ERROR: Trying to add duplicate inventory");
        }
        else
        {
            Inventories.Add(inventoryId, newInventory);
        }
    }
}
