/*  Filename:           TyrannosaurusAttackState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        State for attacking player.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

public class TyrannosaurusAttackState : TyrannosaurusState
{
    private bool _attackCoolDown; 
    private bool _attacking;

    public TyrannosaurusAttackState(TyrannosaurusController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Agent.destination = context.transform.position;

        // Stop walking animation
        context.Idle();

        Attack();
    }

    private void Attack()
    {
        _attackCoolDown = true;
        _attacking = true;
        context.Attack();

        if (context.Timers)
        {
            context.Timers.CreateTimer(context.AttackInterval * 0.5f / 1000f, () => { _attacking = false; });
            context.Timers.CreateTimer(context.AttackInterval / 1000f, () => { _attackCoolDown = false; });
        }
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        if (context.CurrentHealth == 0)
        {
            context.ChangeState(context.DieState);
        }
        else if (!_attacking && _attackCoolDown)
        {
            context.FaceTarget();
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
