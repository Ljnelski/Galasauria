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
    [SerializeField] private CyberBladeContext cyberBladeContext;

    [Header("Transforms")]
    [SerializeField]private Transform blade;
    [SerializeField] private Transform hilt;
    [SerializeField] private Transform handle;

    [Header("Scripts")]
    [SerializeField] private CapsuleCollider hitBox;
    [SerializeField] private Animator animator;

    [Header("SFXs")]
    [SerializeField] private AudioSource primaryAttackSFX;
    [SerializeField] private AudioSource secondaryAttackSFX;

    private void Start()
    {
        ScaleHandle();
        ScaleBlade();
        DeactivateHitBox();
    }
    public override void BeginUse(GameEnums.EquipableInput attack)
    {
        switch(attack)
        {
            case GameEnums.EquipableInput.PRIMARY:
            default:
                animator.SetTrigger("primaryAttack");
                primaryAttackSFX.Play();
                break;
            case GameEnums.EquipableInput.SECONDARY:
                animator.SetTrigger("secondaryAttack");
                secondaryAttackSFX.Play();
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
        cyberBladeContext._power += upgrade.PowerChange;
        cyberBladeContext._handelLength += upgrade.HandelLengthChange;
        cyberBladeContext._bladeLength += upgrade.BladeLengthChange;
        cyberBladeContext._swingSpeed += upgrade.SwingSpeedChange;

        ScaleHandle();
        ScaleBlade();
    }

    public override void EndUse()
    {
        InUse = false;
    }     
    private void ScaleHandle()
    {
        handle.localScale = new Vector3(1, 1 * cyberBladeContext._handelLength, 1);
        hilt.localScale = new Vector3(1, 1 / cyberBladeContext._handelLength, 1);
        blade.localScale = new Vector3(1, 1 / cyberBladeContext._handelLength, 1);
    }

    private void ScaleBlade()
    {
        blade.localScale = new Vector3(1, 1 * cyberBladeContext._bladeLength, 1);
    }

    
}
