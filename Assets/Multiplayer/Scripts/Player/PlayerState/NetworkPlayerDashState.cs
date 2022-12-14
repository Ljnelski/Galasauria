/*  Filename:           NetworkPlayerDashState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class NetworkPlayerDashState : NetworkPlayerState
{
    private bool _dashComplete;
    private Vector3 _dashDirection;

    public NetworkPlayerDashState(NetworkPlayerController networkPlayerController) : base(networkPlayerController)
    {
    }

    public override void OnStateEnter()
    {
        _dashDirection = context.GetFacingPosition() - context.transform.position;
        _dashComplete = false;
        context.Timers.CreateTimer(context.DashDurationMiliseconds / 1000f, () => { _dashComplete = true; });
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
            context.UpdatePlayerActiveStateServerRpc(NetworkPlayerController.eNetworkPlayerState.IDLE);
        }
        else
        {
            context.Rb.velocity = _dashDirection * context.DashSpeed;
        }
    }
}

