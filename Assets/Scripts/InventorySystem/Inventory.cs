/*  Filename:           InventoryManager.cs
 *  Author:             Liam Nelski (301064116), Yuk Yee Wong (301234795)
 *  Last Update:        November 13th, 2022
 *  Description:        Attaches a inventory to the gameobject
 *  Revision History:   October 12, 2022 (Liam Nelski): Initial script.
 *                      November 12, 2022 (Yuk Yee Wong): Replace the notification call with inventoryIncrementAction
 *                      November 13th, 2022 (Liam Nelski): Rename Action and added two more
 *                      December 5th, 2022 (Yuk Yee Wong): Initiate new dictionary when declare.
 */
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public int Id;
    public Dictionary<ItemData, Item> itemDictionary = new Dictionary<ItemData, Item>();
    public List<Item> inventory;
    public Action<ItemData> OnAddItem;
    public Action OnInventoryUpdate;

    public void Start()
    {
        inventory = new List<Item>();
        itemDictionary = new Dictionary<ItemData, Item>();
    }

    public void AddItem(ItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out Item item))
        {
            item.AddToStack();
        }
        else
        {
            Item newItem = new Item(referenceData);
            inventory.Add(newItem);
            itemDictionary.Add(referenceData, newItem);
        }
        OnAddItem?.Invoke(referenceData);
        OnInventoryUpdate?.Invoke();
    }

    public void RemoveItem(ItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out Item targetItem))
        {
            targetItem.RemoveFromStack();

            if (targetItem.stackSize == 0)
            {
                inventory.Remove(targetItem);
                itemDictionary.Remove(referenceData);
            }
        }
        OnInventoryUpdate?.Invoke();
    }

    public void ClearInventory()
    {
        inventory.Clear();
        itemDictionary.Clear();
        OnInventoryUpdate?.Invoke();
    }

    public void CraftItem(RecipeData recipe)
    {
        if (!HasItems(recipe.inputItems))
        {
            return;
        }

        // Create the outputItems
        foreach (RecipeIngredient recipeItem in recipe.outputItems)
        {
            for (int i = 0; i < recipeItem.itemCount; i++)
            {
                AddItem(recipeItem.data);
            }
        }

        // Remove the items the the recipe intakes
        foreach (RecipeIngredient recipeItem in recipe.inputItems)
        {
            for (int i = 0; i < recipeItem.itemCount; i++)
            {
                RemoveItem(recipeItem.data);
            }
        }
    }

    public bool HasItems(List<RecipeIngredient> items)
    {
        // Check if the inventory has the required number of item for the recipe
        foreach (RecipeIngredient recipeItem in items)
        {
            itemDictionary.TryGetValue(recipeItem.data, out Item inventoryItem);
            if (inventoryItem == null || inventoryItem.stackSize < recipeItem.itemCount)
            {
                Debug.Log("Does not have the required number of " + recipeItem.data.itemName);
                return false;
            }
        }

        return true;
    }

    public int GetItemStackSize(ItemData itemData)
    {
        itemDictionary.TryGetValue(itemData, out Item inventoryItem);
        if (inventoryItem == null)
        {
            return 0;
        }
        else
        {
            return inventoryItem.stackSize;
        }
    }
}
