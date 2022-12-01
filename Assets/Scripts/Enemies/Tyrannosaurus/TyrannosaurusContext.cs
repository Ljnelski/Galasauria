/*  Filename:           TyrannosaurusContext.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Scriptable Object to hold Tyrannosaurus Data
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Inital Script.
 */

using UnityEngine;

[CreateAssetMenu(fileName = "TyrannosaurusContext", menuName = "ScriptableObjects/TyrannosaurusContext")]
public class TyrannosaurusContext : ScriptableObject
{
    [Header("Health")]
    public float _maxHealth;
    public float _currentHealth;

    [Header("Movement")]
    public float _baseSpeed;
    public float _turnSpeed;
    public float _acceleration;
    public float _playerOffsetDistance;

    [Header("Attack")]
    public float _chargeMiliseconds;
    public float _attackCoolDownMiliseconds;

    [Header("Die")]
    public float _dieMiliseconds;

    [Header("Damage")]
    public float _baseDamage;

    [Header("Sensory")]
    public float _detectionRange;

    [Header("Score")]
    public int _score;
}
