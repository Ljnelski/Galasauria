/*  Filename:           PlayerController.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November th, 2022
 *  Description:        DashState
 *  Revision History:   November 3rd (Liam Nelski): Moved to its own File.
 *                      November 12th (Liam Nelski): Implemented Timers using cooldown 
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
        _dashDirection = context.FacingLocation.position - context.transform.position;
        _dashComplete = false;
        context.Timers.CreateTimer(context.DashDurationMiliseconds / 1000f, () => { _dashComplete= true; });
        context.Animator.SetTrigger("startDash");
    }

    public override void OnStateExit()
    {
        context.Rb.velocity = Vector3.zero;
        context.CanDash = false;
        context.Timers.CreateTimer(context.DashCoolDownMiliseconds / 1000f,
            () => { context.CanDash = true; },
            (float timeRemaining) => { context.CurrentDashCoolDown = timeRemaining; });
        context.Animator.SetTrigger("endDash");

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

