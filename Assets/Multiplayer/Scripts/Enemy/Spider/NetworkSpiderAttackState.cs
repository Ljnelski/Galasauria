using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSpiderAttackState : NetworkSpiderState
{
    private bool _attackCoolDown;
    private bool _exited;

    public NetworkSpiderAttackState(NetworkSpiderController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Agent.destination = context.transform.position;

        // Stop walking animation
        context.Idle();
    }

    private void Attack()
    {
        if (!_attackCoolDown)
        {
            _attackCoolDown = true;
            context.Attack();
            context.EnableDestroyer(true);

            context.Timers.CreateTimer(context.AttackDuration / 1000f, () => {
                if (!_exited)
                {
                    context.EnableDestroyer(false);
                }
            });

            if (context.Timers)
            {
                context.Timers.CreateTimer(context.AttackInterval / 1000f, () => { _attackCoolDown = false; });
            }
        }
    }

    public override void OnStateExit()
    {
        _exited = true;
        context.EnableDestroyer(false);
    }

    public override void OnStateRun()
    {
        if (!context.ReachedPlayer())
        {
            if (context.PlayerInRange())
            {
                context.UpdateSpiderActiveStateServerRpc(NetworkSpiderController.eNetworkSpiderState.CHASE);
            }
            else
            {
                context.UpdateSpiderActiveStateServerRpc(NetworkSpiderController.eNetworkSpiderState.IDLE);
            }
        }
        else
        {
            context.FaceTarget();

            Attack();
        }
    }
}
