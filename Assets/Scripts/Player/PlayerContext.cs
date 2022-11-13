/*  Filename:           PlayerController.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 12th, 2022
 *  Description:        Scriptable Object to hold playerData
 *  Revision History:   November 12th (Liam Nelski): Inital Script.
 *  
 */
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerContext", menuName ="ScriptableObjects/PlayerContext")]
public class PlayerContext : ScriptableObject
{
    [Header("Health")]
    public float _maxHealth;
    public float _currentHealth;

    [Header("Movement")]
    public float _currentSpeed;
    public float _baseSpeed;
    public float _boostSpeedMultiplier;
    public float _turnSpeed;
    public float _acceleration;

    [Header("Dash")]
    public float _dashSpeed;
    public float _dashDurationMiliseconds;
    public float _dashCoolDownMiliseconds;
    public bool _canDash;
}