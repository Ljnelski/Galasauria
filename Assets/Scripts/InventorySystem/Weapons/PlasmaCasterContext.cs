using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Items/PlasmaCasterContext", fileName = "PlasmaCasterContext")]
public class PlasmaCasterContext : ScriptableObject
{
    [SerializeField] public float _baseDamage;

    [SerializeField] public float _damage;
    [SerializeField] public float _fireRate;
    [SerializeField] public float _range;
    [SerializeField] public float _shotCount; 
}
