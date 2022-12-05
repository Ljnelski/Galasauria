/*  Filename:           RaptorChaseState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 5, 2022
 *  Description:        State for attacking player.
 *  Revision History:   November 27 (Yuk Yee Wong): Initial script.
 *                      December 5, 2022 (Yuk Yee Wong): Used attack duration to disable destroyer
 */

using UnityEngine;

public class RaptorAttackState : RaptorState
{
    private bool _attackCoolDown;
    private bool _exited;

    public RaptorAttackState(RaptorController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Agent.destination = context.transform.position;

        // Stop walking animation
        context.Idle();

        context.EnableDestroyer(true);

        Attack();
    }

    private void Attack()
    {
        _attackCoolDown = true;
        context.Attack();

        if (context.Timers)
        {
            context.Timers.CreateTimer(context.AttackDuration / 1000f, () => {
                if (!_exited)
                {
                    context.EnableDestroyer(false);
                }
            });
            context.Timers.CreateTimer(context.AttackInterval / 1000f, () => { _attackCoolDown = false; });
        }
    }

    public override void OnStateExit()
    {
        _exited = true;
        context.EnableDestroyer(false);
    }

    public override void OnStateRun()
    {
        if (context.CurrentHealth == 0)
        {
            context.ChangeState(context.DieState);
        }
        else if (!_attackCoolDown || !context.ReachedDestination())
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
        }
    }
}
