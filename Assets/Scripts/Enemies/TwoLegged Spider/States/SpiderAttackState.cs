/*  Filename:           SpiderAttackState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 5, 2022
 *  Description:        State for attacking player.
 *  Revision History:   November 25, 2022 (Yuk Yee Wong): Initial script.
 *                      December 5, 2022 (Yuk Yee Wong): Added enable/disable destroyer function and added attack duration
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackState : SpiderState
{
    private bool _attackCoolDown;
    private bool _exited;

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
