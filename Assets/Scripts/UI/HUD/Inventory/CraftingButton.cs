/*  Filename:           CraftingButton.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13, 2022
 *  Description:        Attempts to craft a Recipe
 *  Revision History:   November 13, 2022 (Liam Nelski): Initial script.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CraftingButton : UIPlayerDataReader<Inventory>
{
    [SerializeField] private RecipeData _recipe;
    [SerializeField] private List<InventoryItemDisplay> inventoryItemDisplays;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        GetTargetScript();

        _targetScript.OnInventoryUpdate += Refresh;

        button = GetComponent<Button>();
        button.onClick.AddListener(CraftRecipe);
        button.interactable = false;
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(CraftRecipe);
    }

    private void Refresh()
    {
        for (int i = 0; i < _recipe.inputItems.Count; i++)
        {
            if (inventoryItemDisplays.Count - 1 >= i)
            {
                int itemStackSize = _targetScript.GetItemStackSize(_recipe.inputItems[i].data);
                Debug.Log(itemStackSize);
                inventoryItemDisplays[i].Init(_recipe.inputItems[i], itemStackSize);
            }
            else
            {
                Debug.LogWarning($"CraftingButton ERROR. Count of inventory item display {inventoryItemDisplays.Count} cannot accommodate the amount of input items {i}, please assign enough InventoryItemDisplays.");
            }
        }

        button.interactable = _targetScript.HasItems(_recipe.inputItems);
    }

    public void CraftRecipe()
    {
        _targetScript.CraftItem(_recipe);
    }
}
