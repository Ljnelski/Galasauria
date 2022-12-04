/*  Filename:           SpiderAttackState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        State for attacking player.
 *  Revision History:   November 25, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackState : SpiderState
{
    private bool _attackCoolDown;

    public SpiderAttackState(SpiderController context) : base(context)
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

            if (context.Timers)
            {
                context.Timers.CreateTimer(context.AttackInterval / 1000f, () => { _attackCoolDown = false; });
            }
        }
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        if (!context.ReachedPlayer())
        {
            if (context.PlayerInRange())
            {
                context.ChangeState(context.ChaseState);
            }
            else
            {
                context.ChangeState(context.IdleState);
            }
        }
        else
        {
            context.FaceTarget();

            Attack();
        }
    }
}
