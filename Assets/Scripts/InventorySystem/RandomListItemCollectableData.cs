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

    public void SpawnRandomItem(Vector3 spawnPosition)
    {
        foreach (RandomItemAmount randomItemAmount in randomItemCountData)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= randomItemAmount.chance)
            {
                if (randomItemAmount.itemAmount.itemData.prefab != null)
                {
                    GameObject itemObject = Instantiate(randomItemAmount.itemAmount.itemData.prefab);
                    itemObject.transform.position = spawnPosition;
                }
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

