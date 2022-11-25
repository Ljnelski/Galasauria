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

        // Rotate Legs
        Quaternion rotation = Quaternion.LookRotation(context.Rb.velocity.normalized);
        context.Rb.MoveRotation(Quaternion.RotateTowards(context.transform.rotation, rotation, 360));

        // Move Character
        Vector3 movementVector = Vector3.Lerp(context.Rb.velocity, movementInputTo3DSpace * context.BaseSpeed, context.Acceleration);
        context.Rb.velocity = movementVector;

    }

    protected void RotatePlayer()
    {
        // Rotate Top half
        Quaternion rotation = Quaternion.LookRotation(context.LookAtPosition) ;
        context.seat.rotation = Quaternion.RotateTowards(context.seat.rotation, rotation, context.TurnSpeed);        
    }
}

