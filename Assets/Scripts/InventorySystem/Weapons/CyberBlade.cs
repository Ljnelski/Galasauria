/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116), Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        Controls CyberBlade
 *  Revision History:   October 16, 2022 (Liam Nelski): Initial script.
 *                      November 25, 2022 (Yuk Yee Wong): Remove IDamagingObject which causes bug when attach to the blade. The blade will use Destroyer script instead.
 */
using System;
using UnityEngine;
public class CyberBlade :  EquipableItem
{
    [Header("CyberBladeContext")]
    [SerializeField] private CyberBladeContext CyberBladeData;

    [Header("Transforms")]
    [SerializeField]private Transform blade;
    [SerializeField] private Transform hilt;
    [SerializeField] private Transform handle;

    [Header("Scripts")]
    [SerializeField] private CapsuleCollider hitBox;
    [SerializeField] private Animator animator;   

    public override void BeginUse(GameEnums.EquipableInput attack)
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

    public void ActivateHitBox()
    {
        hitBox.enabled = true;
    }

    public void DeactivateHitBox()
    {
        hitBox.enabled = false;
    }

    public void Upgrade(CyberBladeUpgrade upgrade)
    {
        CyberBladeData = CyberBladeData + upgrade.Data;
    }

    public override void EndUse()
    {
        InUse = false;
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
