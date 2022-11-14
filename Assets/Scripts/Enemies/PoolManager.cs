/*  Filename:           PoolManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        For creating object pooling
 *  Revision History:   August 1, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

public abstract class PoolManager<T> : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;

    [Header("Debug")]
    [SerializeField] private int totalPooledObject;
    [SerializeField] private Queue<GameObject> poolQueue;

    private void Start()
    {
        InitiatePool();
    }

    private void InitiatePool()
    {
        // create empty queue structure
        poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreatePooledObject(true);
        }
    }

    private GameObject CreatePooledObject(bool enqueue)
    {
        var tempObject = Instantiate(prefab, transform);
        tempObject.SetActive(false);

        if (enqueue)
            poolQueue.Enqueue(tempObject);

        totalPooledObject++;

        return tempObject;
    }

    protected T GetPooledObject(Vector3 spawnPosition, bool active)
    {
        GameObject pooledObject = null;
        if (!poolQueue.TryDequeue(out pooledObject))
        {
            pooledObject = CreatePooledObject(false);
        }

        pooledObject.SetActive(active);
        pooledObject.transform.position = spawnPosition;

        return pooledObject.GetComponent<T>();
    }

    protected void ReturnPooledObject(GameObject returnedObject)
    {
        returnedObject.SetActive(false);
        poolQueue.Enqueue(returnedObject);
    }

    public void ReturnAllPooledObjects()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<T>() != null && child.gameObject.activeInHierarchy)
                ReturnPooledObject(child.gameObject);
        }
    }
}
