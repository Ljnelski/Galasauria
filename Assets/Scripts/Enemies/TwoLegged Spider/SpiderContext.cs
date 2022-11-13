using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpiderContext", menuName = "ScriptableObjects/SpiderContext")]
public class SpiderContext : ScriptableObject
{
    [Header("Health")]
    public float _maxHealth;
    public float _currentHealth;

    [Header("Movement")]
    public float _baseSpeed;
    public float _turnSpeed;
    public float _acceleration;

    [Header("Damage")]
    public float _baseDamage;

    [Header("Sensory")]
    public float _detectionRange;
}
