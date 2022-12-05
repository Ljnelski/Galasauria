using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberBladeContext : ScriptableObject
{
    [SerializeField] public float _baseDamage;

    [SerializeField] public float _power;
    [SerializeField] public float _handelLength;
    [SerializeField] public float _bladeLength;
    [SerializeField] public float _swingSpeed;

    public static CyberBladeContext operator +(CyberBladeContext cbd1, CyberBladeContext cbd2)
    {
        cbd1._power += cbd2._power;
        cbd1._handelLength += cbd2._handelLength;
        cbd1._bladeLength += cbd2._bladeLength;
        cbd1._swingSpeed += cbd2._swingSpeed;

        return cbd1;
    }    
}
