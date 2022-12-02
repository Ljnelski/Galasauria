﻿/*  Filename:           PlayerController.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        Controls the player
 *  Revision History:   October 10th (Liam Nelski): Inital Script.
 *                      October 16th (Liam Nelski): Added Use of Equipable Items
 *                      November 3th (Liam Nelski): Moved Equipable from interface to script
 *                      November 3th (Liam Nelski): Made Player Point to the mouse
 */
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController playerController) : base(playerController)
    {

    }
    public override void OnStateEnter()
    {
        ;
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        MovePlayer();
  
        if(context.EquipedItem.ItemEquiped && context.AttackInput != GameEnums.EquipableInput.NONE && !context.EquipedItem.InUse)
        {
            context.ChangeState(context.useItemState);
        }

        if(context.DashInput && context.CanDash)
        {
            context.ChangeState(context.dashState);
        }

        
        float moveSpeedClamped = context.Rb.velocity.magnitude / context.BaseSpeed;
        if (moveSpeedClamped < 0.01f)
            moveSpeedClamped = 0f;

        //Debug.Log("CurrentSpeed: " + context.Rb.velocity.magnitude);
        //Debug.Log("Base Speed: " + context.BaseSpeed);
        //Debug.Log("to animator: " + moveSpeedClamped);



        context.Animator.SetFloat("speed", moveSpeedClamped);
    }
}

