/*  Filename:           CrateContext.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Scriptable Object to hold crateData
 *  Revision History:   November 26 (Yuk Yee Wong): Inital Script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrateContext", menuName = "ScriptableObjects/CrateContext")]
public class CrateContext : ScriptableObject
{
    [Header("Health")]
    public float _maxHealth;
    public float _currentHealth;

    [Header("Interaction")]
    public float _interactionDistance;
}
