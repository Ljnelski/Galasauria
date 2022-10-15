using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public Dictionary<ItemData, Item> itemDictionary;
    public List<Item> inventory;

    public void Awake()
    {
        inventory = new List<Item>();
        itemDictionary = new Dictionary<ItemData, Item>();
    }

    public void AddItem(ItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out Item item))
        {
            item.AddToStack();
            Debug.Log("Adding to: " + item.stackSize);
        }
        else
        {
            Item newItem = new Item(referenceData);
            inventory.Add(newItem);
            itemDictionary.Add(referenceData, newItem);
            Debug.Log("Adding first: " + referenceData.name);
        }
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
