using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Items/RandomListItemCollectableData", fileName = "RandomListItemCollectableData")]
public class RandomListItemCollectableData : ScriptableObject, ICollectable
{
    [SerializeField] private List<RandomItemAmount> randomItemCountData;

    public void Collect(Inventory inventory)
    {
        foreach (RandomItemAmount randomItemAmount in randomItemCountData)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= randomItemAmount.chance)
            {
                inventory.AddItem(randomItemAmount.itemAmount.itemData);
            }
        }
    }
}

[Serializable]
public struct RandomItemAmount
{
    [Range(0f, 1f)]
    [SerializeField] public float chance;
    [SerializeField] public ItemAmount itemAmount;
}

