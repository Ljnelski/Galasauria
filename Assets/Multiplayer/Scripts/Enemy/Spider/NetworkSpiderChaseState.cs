/*  Filename:           NetworkSpiderChaseState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

public class NetworkSpiderChaseState : NetworkSpiderState
{
    public NetworkSpiderChaseState(NetworkSpiderController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Walk();
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        context.Agent.destination = context.ChaseTarget.position;

        if (!context.PlayerInRange())
        {
            context.UpdateSpiderActiveStateServerRpc(NetworkSpiderController.eNetworkSpiderState.IDLE);
        }
        else if (context.ReachedPlayer())
        {
            context.UpdateSpiderActiveStateServerRpc(NetworkSpiderController.eNetworkSpiderState.ATTACK);
        }
    }
}