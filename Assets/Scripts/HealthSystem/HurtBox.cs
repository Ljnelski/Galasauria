using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.HealthSystem
{
    public class HurtBox : MonoBehaviour
    {
        LayerMask TargetLayer;

        Action OnOnTriggerEnter;
        Action OnOnTriggerEnterTargetLayer;

        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.layer = TargetLayer;
        }
    }
}