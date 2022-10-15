/*  Filename:           InventoryManager.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Attaches a inventory to the gameobject
 *  Revision History:   October 12, 2022 (Liam Nelski): Initial script. *                     
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public int Id;

    public Dictionary<ItemData, Item> itemDictionary;
    public List<Item> inventory;


    public void Start()
    {
        inventory = new List<Item>();
        itemDictionary = new Dictionary<ItemData, Item>();

        InventoryManager.Instance.AssignInventory(Id, GetComponent<Inventory>());
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
        NotificationController.Instance.Notify(referenceData.icon, referenceData.name, 1);
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
    }

    public void ClearInventory()
    {
        inventory.Clear();
        itemDictionary.Clear();
    }

    public void CraftItem(RecipeData recipe)
    {
        // Check if the inventory has the required number of item for the recipe
        foreach (Recipe recipeItem in recipe.inputItems)
        {
            itemDictionary.TryGetValue(recipeItem.data, out Item inventoryItem);  
            if (inventoryItem == null || inventoryItem.stackSize < recipeItem.itemCount)
            {
                Debug.Log("Does not have the required number of " + recipeItem.data.name);
                return;
            }
        }

        // Create the outputItems
        foreach(Recipe recipeItem in recipe.outputItems)
        {
            for (int i = 0; i < recipeItem.itemCount; i++)
            {
                AddItem(recipeItem.data);
            }            
        }

        // Remove the items the the recipe intakes
        foreach (Recipe recipeItem in recipe.inputItems)
        {
            for (int i = 0; i < recipeItem.itemCount; i++)
            {
                RemoveItem(recipeItem.data);
            }
        }


    }
}

public interface IEquipable<TUpgrade> where TUpgrade : Enum
{
    public Action Attack { get; }
    public abstract void Upgrade();
}
