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

[CreateAssetMenu(menuName = "Items/Recipe", fileName ="Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public List<RecipeIngredient> inputItems;
    public List<RecipeIngredient> outputItems;
}

[Serializable]
public struct RecipeIngredient
{
    [SerializeField]
    public ItemData data;
    [SerializeField]
    public int itemCount;
}

