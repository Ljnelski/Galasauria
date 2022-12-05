using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCasterContext : ScriptableObject
{
    [SerializeField] public float _baseDamage;

    [SerializeField] public float _damage;
    [SerializeField] public float _fireRate;
    [SerializeField] public float _range;
    [SerializeField] public float _shotCount;

    public static PlasmaCasterContext operator +(PlasmaCasterContext pcd1, PlasmaCasterContext pcd2)
    {
        pcd1._damage += pcd2._damage;
        pcd1._fireRate += pcd2._fireRate;
        pcd1._range += pcd2._range;
        pcd1._shotCount += pcd2._shotCount;

        return pcd1;
    }
}
