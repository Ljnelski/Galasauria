/*  Filename:           NetworkSpiderIdleState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

public class NetworkSpiderIdleState : NetworkSpiderState
{
    public NetworkSpiderIdleState(NetworkSpiderController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Agent.isStopped = true;
        context.Idle();
    }

    public override void OnStateExit()
    {
        context.Agent.isStopped = false;
    }

    public override void OnStateRun()
    {
        if (context.PlayerInRange())
        {
            context.UpdateSpiderActiveStateServerRpc(NetworkSpiderController.eNetworkSpiderState.CHASE);
        }
    }
}

