using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpiderHive : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private GameObject HiveMesh;
    [SerializeField] private GameObject BrokenHiveMesh;

    [SerializeField] private float _hiveDetectionRange;
    [SerializeField] private float _hiveHealth;

    private void Awake()
    {
        sphereCollider.radius = _hiveDetectionRange;
        _healthSystem.ReceiveDamage += TakeDamage;
    }

    private void TakeDamage(float Damage)
    {
        _hiveHealth -= Damage;

        if(_hiveHealth < 0)
        {
            _hiveHealth = 0;

            sphereCollider.enabled = false;
            spawner.StopSpawn();

            HiveMesh.SetActive(false);
            BrokenHiveMesh.SetActive(true);

            _healthSystem.ReceiveDamage -= TakeDamage;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Exit" + other.name);
        spawner.StartSpawn();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit " + other.name);
        spawner.StopSpawn();
    }
}
