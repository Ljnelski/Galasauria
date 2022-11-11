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
/*  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        Basic playerState that all other can inherit from to reducee duplicate code
 */
public abstract class PlayerState : BaseState<PlayerController>
{
    public PlayerState(PlayerController playerController) : base(playerController)
    {
        ;
    }

    protected void MovePlayer()
    {
        context.Rb.velocity = Vector3.Slerp(context.Rb.velocity, new Vector3(context.MovementInput.x, 0, context.MovementInput.y) * context.baseSpeed, 0.1f);
    }

    protected void RotatePlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(context.LookAtPosition);
        context.Rb.MoveRotation(Quaternion.RotateTowards(context.transform.rotation, rotation, context.turnSpeed));
    }
}

