/*  Filename:           TyrannosaurusAttackState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 5, 2022
 *  Description:        State for attacking player.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 *                      December 5, 2022 (Yuk Yee Wong): Added enable/disable destroyer and used attack duration
 */

public class TyrannosaurusAttackState : TyrannosaurusState
{
    private bool _attackCoolDown; 
    private bool _attacking;
    private bool _exited;

    public TyrannosaurusAttackState(TyrannosaurusController context) : base(context)
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
        _attacking = true;
        context.Attack();

        if (context.Timers)
        {
            context.Timers.CreateTimer(context.AttackDuration / 1000f, () => { 
                _attacking = false; 
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
        else if (!_attacking && _attackCoolDown)
        {
            //context.FaceTarget();
            //context.Run();
        }
        else if (!_attackCoolDown)
        {
            if (context.PlayerInRange())
            {
                context.ChangeState(context.ChargeState);
            }
            else
            {
                context.ChangeState(context.IdleState);
            }
        }
    }
}
