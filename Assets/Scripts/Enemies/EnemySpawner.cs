using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpiderPoolManager spiderPoolManager;
    [SerializeField] private float interval;

    private bool startedSpawn;
    private float timePassed;

    private void Start()
    {
        timePassed = interval;
    }

    private void Update()
    {
        if (startedSpawn)
        {
            timePassed += Time.deltaTime;

            if (timePassed > interval)
            {
                Spawn();
                timePassed = 0;
            }
        }
    }

    public void StartSpawn()
    {
        startedSpawn = true;
    }

    private void Spawn()
    {
        spiderPoolManager.GetPooledEnemy(transform.position);
    }
}
