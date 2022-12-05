/*  Filename:           RaptorContext.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 5, 2022
 *  Description:        Scriptable Object to hold Raptor Data
 *  Revision History:   November 27, 2022 (Yuk Yee Wong): Inital Script.
 *                      December 5, 2022 (Yuk Yee Wong): Added attack duration
 */

using UnityEngine;

[CreateAssetMenu(fileName = "RaptorContext", menuName = "ScriptableObjects/RaptorContext")]
public class RaptorContext : ScriptableObject
{
    [Header("Health")]
    public float _maxHealth;
    public float _currentHealth;

    [Header("Movement")]
    public float _baseSpeed;
    public float _turnSpeed;
    public float _acceleration;
    public float _destinationOffsetDistance;
    public float _playerOffsetDistance;

    [Header("Attack")]
    public float _attackCoolDownMiliseconds; 
    public float _attackDuration;

    [Header("Die")]
    public float _dieMiliseconds;

    [Header("Damage")]
    public float _baseDamage;

    [Header("Sensory")]
    public float _detectionRange;

    [Header("Score")]
    public int _score;
}
