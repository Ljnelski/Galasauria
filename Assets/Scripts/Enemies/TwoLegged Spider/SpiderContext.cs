/*  Filename:           SpiderContext.cs
 *  Author:             Liam Nelski (301064116), Yuk Yee Wong (301234795)
 *  Last Update:        December 5th, 2022
 *  Description:        Scriptable Object to hold spiderData
 *  Revision History:   November 13th (Liam Nelski): Inital Script.
 *                      November 25th (Yuk Yee Wong): Added player offset distance and attack cool down miliseconds.
 *                      December 5th, 2022 (Yuk Yee Wong): Added attack duration
 */

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
    public float _playerOffsetDistance;

    [Header("Attack")]
    public float _attackCoolDownMiliseconds;
    public float _attackDuration;

    [Header("Damage")]
    public float _baseDamage;

    [Header("Sensory")]
    public float _detectionRange;

    [Header("Score")]
    public int _score;
}
