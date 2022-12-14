using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

