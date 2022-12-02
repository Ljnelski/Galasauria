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
        Vector3 movementInputTo3DSpace = new Vector3(context.MovementInput.x, 0, context.MovementInput.y);

        // Move Character
        Vector3 movementVector = Vector3.Lerp(context.Rb.velocity, movementInputTo3DSpace * (context.BaseSpeed + 1 - context.Acceleration), context.Acceleration);
        context.Rb.velocity = movementVector;


        // Capture the direction to which the legs have to face
        if (movementInputTo3DSpace.magnitude > 0.01f)
        {
            context.lastDirectionFaced = context.Rb.velocity.normalized;
        }

        // Rotate The legs to the last DirectionFaced
        context.FacingLocation.localPosition = Vector3.Slerp(
                context.FacingLocation.localPosition,
                context.lastDirectionFaced,
                context.TurnSpeed).normalized;

    }
}

