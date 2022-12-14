using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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