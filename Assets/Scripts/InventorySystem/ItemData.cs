using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Items/Item Data", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    public string id;
    public string itemName;
    public int maxStack;
    public int itemWeight;
    public GameObject prefab;
}


