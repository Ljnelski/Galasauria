/*  Filename:           TestNavMeshAgent.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        July 22, 2023
 *  Description:        Controls all the logic that the spider hives need
 *  Revision History:   July 22, 2023 (Liam Nelski): Initial script.
 */
using UnityEngine;

public class SpiderHive : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private PlayerDetector _playerDetector;

    [SerializeField] private GameObject _hiveObject;
    [SerializeField] private GameObject _brokenHiveObject;

    [SerializeField] private float _hiveHealth;
    [SerializeField] private float _hiveDetectionRange;

    private void Start()
    {
        _healthSystem.ReceiveDamage += TakeDamage;
        _playerDetector.SetDetectionRadius(_hiveDetectionRange);

        _playerDetector.PlayerEntered += PlayerDetected;
        _playerDetector.PlayerExited += PlayerUndetected;
    }

    private void PlayerDetected(PlayerController playerController)
    {
        if (_hiveHealth > 0f)
        {
            _spawner.StartSpawn();
        }
    }
    private void PlayerUndetected(PlayerController playerController)
    {
        _spawner.StopSpawn();
    }   

    private void TakeDamage(float Damage)
    {
        Debug.Log("Take Damage Called from SpiderHive");
        _hiveHealth -= Damage;

        if(_hiveHealth < 0)
        {
            _hiveHealth = 0;

            _spawner.StopSpawn();

            _hiveObject.SetActive(false);
            _playerDetector.gameObject.SetActive(false);
            _brokenHiveObject.SetActive(true);

            _healthSystem.ReceiveDamage -= TakeDamage;

            _playerDetector.PlayerEntered -= PlayerDetected;
            _playerDetector.PlayerExited -= PlayerUndetected;
        }
    }
}
