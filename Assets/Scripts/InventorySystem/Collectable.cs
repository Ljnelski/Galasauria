using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    public ItemData item;
    
    public void collectItem(Inventory inventory)
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }
}
