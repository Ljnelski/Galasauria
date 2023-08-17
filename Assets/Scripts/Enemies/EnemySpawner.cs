/*  Filename:           PLayerDetector.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        July 22, 2022
 *  Description:        Spawns Enemies
 *  Revision History:   July 22, 2023 (Liam Nelski): Inital Script.
 *      `               July 22, 2023 (Liam Nelski): Added Transform for spawn position
 */
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpiderPoolManager _spiderPoolManager;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private float _spawnInterval;

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
        StopCoroutine(SpawnEnemy());
    }

    private void Spawn()
    {
        if (_spawnLocation == null)
        {
            _spiderPoolManager.GetPooledEnemy(transform.position);
        } else
        {
            _spiderPoolManager.GetPooledEnemy(_spawnLocation.position);
        }
    }

    private IEnumerator SpawnEnemy()
    {        
        while (startedSpawn)
        {
            Spawn();
            yield return new WaitForSeconds(_spawnInterval);
        }

        yield break;
    }
}
