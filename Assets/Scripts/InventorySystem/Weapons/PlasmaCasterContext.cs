using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlasmaCasterContext : ScriptableObject
{
    [SerializeField] public float _baseDamage;

    [SerializeField] public float _damage;
    [SerializeField] public float _fireRate;
    [SerializeField] public float _range;
    [SerializeField] public float _shotCount; 
}
