/*  Filename:           RaptorChaseState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 27, 2022
 *  Description:        State for chasing player
 *  Revision History:   November 27 (Yuk Yee Wong): Initial script.
 */

public class RaptorChaseState : RaptorState
{
    public RaptorChaseState(RaptorController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.Chase();
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        context.UpdateDestination();
        context.Agent.destination = context.Destination;

        if (context.CurrentHealth == 0)
        {
            context.ChangeState(context.DieState);
        }
        else if (!context.PlayerInRange())
        {
            context.ChangeState(context.IdleState);
        }
        else if (context.ReachedDestination())
        {
            context.ChangeState(context.AttackState);
        }
        else
        {
            //context.FaceTarget();
        }
    }
}
