using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrateContext", menuName = "ScriptableObjects/CrateContext")]
public class CrateContext : ScriptableObject
{
    [Header("Health")]
    public float _maxHealth;
    public float _currentHealth;

    [Header("Interaction")]
    public float _interactionDistance;
}
