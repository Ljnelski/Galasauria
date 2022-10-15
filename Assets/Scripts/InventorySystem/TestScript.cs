using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject player;
    public RecipeData recipe;

    private Inventory playerInventory;

    private void Awake()
    {
        playerInventory = player.GetComponent<Inventory>();
    }

    public void CraftTestItem()
    {
        playerInventory.CraftItem(recipe);
    }

}
