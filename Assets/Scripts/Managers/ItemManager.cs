/*  Filename:           ItemManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Loads static data from resources, builds dictionaries, and returns static item data by item type
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public Dictionary<GameEnums.ItemType, ItemData> ItemDictionary { get; private set; }

    public bool Inited { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Load items from resources
        ItemDictionary = new Dictionary<GameEnums.ItemType, ItemData>();
        ItemStaticData itemStaticData = Resources.Load<ItemStaticData>("ScriptableObjects/ItemStaticData");
        if (itemStaticData != null)
        {
            foreach (ItemData itemData in itemStaticData.itemList)
            {
                ItemDictionary.Add(itemData.type, itemData);
            }
        }

        Inited = true;
    }

    public ItemData GetItemData(GameEnums.ItemType itemType)
    {
        if (Inited && ItemDictionary.ContainsKey(itemType))
        {
            return ItemDictionary[itemType];
        }
        return null;
    }
}
