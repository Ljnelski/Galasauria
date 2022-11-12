/*  Filename:           PlayerController.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November th, 2022
 *  Description:        Controls the player
 *  Revision History:   October 10th (Liam Nelski): Moved to its own File.
 */
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float currentTime = 0f;
    private Vector3 dashDirection;

    public PlayerDashState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnStateEnter()
    {
        dashDirection = new Vector3(context.MovementInput.x, 0.0f, context.MovementInput.y);
        currentTime = 0f;
    }

    public override void OnStateExit()
    {
        context.Rb.velocity = Vector3.zero;
    }

    public override void OnStateRun()
    {
        currentTime += Time.fixedDeltaTime * 1000;
        if (currentTime >= context.dashDurationMiliseconds)
        {
            context.ChangeState(context.idleState);
        }
        else
        {
            context.Rb.velocity = dashDirection * context.dashSpeed;
        }
    }
}

