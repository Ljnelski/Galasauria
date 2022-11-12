using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerContext", menuName ="ScriptableObjects/PlayerContext")]
public class PlayerContext : ScriptableObject
{
    [Header("Health")]
    public float _maxHealth;
    public float _currentHealth;

    [Header("Movement")]
    public float _baseSpeed;
    public float _turnSpeed;
    public float _acceleration;

    [Header("Dash")]
    public float _dashSpeed;
    public float _dashDurationMiliseconds;
    public float _dashCoolDownMiliseconds;
}