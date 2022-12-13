/*  Filename:           RaptorContext.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        Scriptable Object to hold Apatosaurus Data
 *  Revision History:   December 12, 2022 (Yuk Yee Wong): Inital Script.
 */

using UnityEngine;

[CreateAssetMenu(fileName = "ApatosaurusContext", menuName = "ScriptableObjects/ApatosaurusContext")]
public class ApatosaurusContext : ScriptableObject
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

    [Header("Die")]
    public float _dieMiliseconds;

    [Header("Sensory")]
    public float _detectionRange;

    [Header("Score")]
    public int _score;

    [Header("Random Rewards")]
    public RandomListItemCollectableData _randomCollectable;
}
