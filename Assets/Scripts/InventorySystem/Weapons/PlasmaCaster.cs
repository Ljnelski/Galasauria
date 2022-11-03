/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 16, 2022
 *  Description:        Controls Plasma Caster
 *  Revision History:   October 16, 2022 (Liam Nelski): Initial script. *                     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCaster : MonoBehaviour, IEquipable
{
    public bool InUse { get; private set; }

    private Transform barrel;


    private void Awake()
    {       
        barrel = transform.GetChild(0);
    }

    public void BeginUse(GameEnums.EquipableInput attack)
    {
        throw new System.NotImplementedException();
    }

    public void EndUse()
    {
        throw new System.NotImplementedException();
    
    }

    private void ExtendBarrel(float Distance)
    {
        barrel.position = new Vector3(barrel.position.x, barrel.position.y, Distance);
    }
}
