/*  Filename:           PlayerController.cs
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
        Debug.Log("IDLE STATE");
        MovePlayer();
  
        if(context.EquipedItem.ItemEquiped && context.AttackInput != GameEnums.EquipableInput.NONE && !context.EquipedItem.InUse)
        {
            context.ChangeState(context.useItemState);
        }

        if(context.DashInput && context.DashCoolDownTimer <= 0)
        {
            context.ChangeState(context.dashState);
        }

        // Look At Mouse Position
        RotatePlayer();
    }
}

