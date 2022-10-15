using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Items/Recipe", fileName ="Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public List<Recipe> inputItems;
    public List<Recipe> outputItems;
}

[Serializable]
public struct Recipe
{
    [SerializeField]
    public ItemData data;
    [SerializeField]
    public int itemCount;
}

