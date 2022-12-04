/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        December 3rd, 2022
 *  Description:        Plasmabolt Script
 *  Revision History:   December 3rd, 2022 (Liam Nelski): Initial script. 
 */
using Assets.Scripts.InventorySystem.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasmabolt : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public PlasmaboltPoolManager plasmaboltPool;
    private Destroyer destroyer;

    public void OnEnable()
    {
        destroyer = GetComponent<Destroyer>();
        destroyer.OnTargetDestroyedAction += Disable;
    }

    public void OnDisable()
    {
        destroyer.OnTargetDestroyedAction -= Disable;
    }

    public void Disable(int score)
    {
        Debug.Log("Disabling: " + score);
        plasmaboltPool.ReturnPooledPlasmabolt(this);
    }

    public void Shoot(float speed)
    {
        _rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        transform.SetParent(null);
    }
}
