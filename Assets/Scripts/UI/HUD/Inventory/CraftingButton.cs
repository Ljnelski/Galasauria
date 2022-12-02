/*  Filename:           CraftingButton.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13, 2022
 *  Description:        Attempts to craft a Recipe
 *  Revision History:   November 13, 2022 (Liam Nelski): Initial script.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : UIPlayerDataReader<Inventory>
{
    [SerializeField] private RecipeData _recipe;
    // Start is called before the first frame update
    void Start()
    {
        GetTargetScript();
    }

    public void CraftRecipe()
    {
        _targetScript.CraftItem(_recipe);
    }
}
