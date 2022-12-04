/*  Filename:           HealthSystem.cs
 *  Author:             Jinkyu Choi (301024988), Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        Script which manages the health of the gameObject
 *  Revision History:   November 11, 2022 (Jinkyu Choi): Initial Script.
 *                      November 13, 2022 (Jinkyu Choi): Changed Script based on Action
 *                      November 25, 2022 (Yuk Yee Wong): Replace IDestroyingObject to IDestroyer and assign the die action
 */

using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Action<float> ReceiveDamage;
    public Action<int> Die;
    public string DamageTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(DamageTag))
        {
            IDestroyer iDestroyer = other.GetComponent<IDestroyer>();
            if (iDestroyer == null)
            {
                Debug.LogError("HealthSystem ERROR: Cannot find IDestroyer in damaging object, check the script for damaging object");
            }
            else
            {
                Die = null;
                Die += iDestroyer.OnTargetDestroyed;

                ReceiveDamage?.Invoke(other.GetComponent<IDestroyer>().Damage);
            }
        }
    }
}
