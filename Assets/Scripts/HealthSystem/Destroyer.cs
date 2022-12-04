/*  Filename:           Destroyer.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        Concrete clss for IDestroyer to implement damage and action.
 *  Revision History:   November 25, 2022 (Yuk Yee Wong): Initial script.
 */

using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Destroyer : MonoBehaviour, IDestroyer
{
    [SerializeField] private float damage;
    public float Damage { get => damage; set => damage = value; }

    public Action<int> OnTargetDestroyedAction;

    public void OnTargetDestroyed(int score)
    {
        OnTargetDestroyedAction?.Invoke(score);
    }
}
