using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpiderPoolManager spiderPoolManager;
    [SerializeField] private float interval;

    private bool startedSpawn = false;
    
    public void StartSpawn()
    {
        Debug.Log("Starting Spawn:" + !startedSpawn);
        if(!startedSpawn)
        {
            startedSpawn = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    public void StopSpawn()
    {
        startedSpawn = false;
    }

    private void Spawn()
    {
        spiderPoolManager.GetPooledEnemy(transform.position);
    }

    private IEnumerator SpawnEnemy()
    {
        while (startedSpawn)
        {
            Spawn();
            yield return new WaitForSeconds(interval);
        }
    }
}
