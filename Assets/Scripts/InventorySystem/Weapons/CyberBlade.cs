/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 16, 2022
 *  Description:        Controls CyberBlade
 *  Revision History:   October 16, 2022 (Liam Nelski): Initial script. *  =
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CyberBlade : MonoBehaviour, IEquipable, IDamagingObject
{
    public bool InUse { get; private set; }
    public float Damage { get; set; }

    private Transform blade;
    private Transform hilt;
    private Transform handle;
    private CapsuleCollider hitBox;
    private Animator animator;

    private void Awake()
    {
        handle = transform.GetChild(0).GetChild(0).GetChild(0);
        hilt = handle.GetChild(0);
        blade = hilt.GetChild(0);

        animator = GetComponent<Animator>();
        hitBox = blade.GetComponent<CapsuleCollider>();

        InUse = false;
        Damage = 100;
    }

    public void BeginUse(GameEnums.EquipableInput attack)
    {
        switch(attack)
        {
            case GameEnums.EquipableInput.PRIMARY:
            default:
                animator.SetTrigger("primaryAttack");
                break;
            case GameEnums.EquipableInput.SECONDARY:
                animator.SetTrigger("secondaryAttack");
                break;
        }
        InUse = true;
    }

    public void FinishAttack()
    {
        InUse = false;
    }

    public void ActivateHitBox()
    {
        hitBox.enabled = true;
    }

    public void DeactivateHitBox()
    {
        hitBox.enabled = false;
    }

    public void Upgrade()
    {
        throw new NotImplementedException();
    }

    public void EndUse()
    {
        throw new NotImplementedException();
    }

    private void ScaleHandle(float factor)
    {
        handle.localScale = new Vector3(handle.localScale.x, handle.localScale.y * factor, handle.localScale.z);
        hilt.localScale = new Vector3(handle.localScale.x, handle.localScale.y / factor, handle.localScale.z);
        blade.localScale = new Vector3(blade.localScale.x, blade.localScale.y / factor, blade.localScale.z);
    }

    private void ScaleBlade(float factor)
    {
        blade.localScale = new Vector3(blade.localScale.x, blade.localScale.y * factor, blade.localScale.z);
    }

    
}
