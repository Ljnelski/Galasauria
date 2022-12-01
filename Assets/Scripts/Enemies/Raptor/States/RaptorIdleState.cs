/*  Filename:           RaptorIdleState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 27, 2022
 *  Description:        Raptor Idle State
 *  Revision History:   November 27 (Yuk Yee Wong): Initial script.
 */

public class RaptorIdleState : RaptorState
{
    public RaptorIdleState(RaptorController context) : base(context)
    {
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
        if (context.CurrentHealth == 0)
        {
            context.ChangeState(context.DieState);
        }
        else if (context.PlayerInRange())
        {
            context.ChangeState(context.ChaseState);
        }
    }
}
