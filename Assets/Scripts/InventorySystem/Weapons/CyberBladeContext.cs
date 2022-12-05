using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CyberBladeContext : ScriptableObject
{
    [SerializeField] public float _baseDamage;

    [SerializeField] public float _power;
    [SerializeField] public float _handelLength;
    [SerializeField] public float _bladeLength;
    [SerializeField] public float _swingSpeed;  
}
