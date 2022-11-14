/*  Filename:           HealthSystem.cs
 *  Author:             Jinkyu Choi (301024988)
 *  Last Update:        November 13, 2022
 *  Description:        Script which manages the health of the gameObject
 *  Revision History:   November, 11, 2022 (Jinkyu Choi): Initial Script.
 *                      November, 13, 2022 (Jinkyu Choi): Changed Script based on Action
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public event Action<float> damaged;
    public string DamageTag;
    private void OnTriggerEnter(Collider other)
    {    
        if (other.gameObject.CompareTag(DamageTag))
        {
            if (other.GetComponent<IDamagingObject>() == null)
            {
                Debug.LogError("HealthSystem ERROR: Cannot find IDamageingObject in damaging object, check the script for damaging object");
            }
            else
            {
                damaged?.Invoke(other.GetComponent<IDamagingObject>().Damage);
            }
        }
    }
}
