/*  Filename:           PlayerController.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November th, 2022
 *  Description:        Controls the player
 *  Revision History:   October 10th (Liam Nelski): Moved to its own File.
 */
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private bool _dashComplete;
    private Vector3 _dashDirection;

    public PlayerDashState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnStateEnter()
    {
        _dashDirection = new Vector3(context.MovementInput.x, 0.0f, context.MovementInput.y);
        _dashComplete = false;
        context.Timers.CreateTimer(context.DashDurationMiliseconds / 1000f, () => { _dashComplete= true; });
    }

    public override void OnStateExit()
    {
        context.Rb.velocity = Vector3.zero;
        context.CanDash = false;
        context.Timers.CreateTimer(context.DashCoolDownMiliseconds / 1000f,
            () => { context.CanDash = true; },
            (float timeRemaining) => { context.CurrentDashCoolDown = timeRemaining; });
    }

    public override void OnStateRun()
    {
        if (_dashComplete)
        {
            context.ChangeState(context.idleState);
        }
        else
        {
            context.Rb.velocity = _dashDirection * context.DashSpeed;
        }
    }
}

