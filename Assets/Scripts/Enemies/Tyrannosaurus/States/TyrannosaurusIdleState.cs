/*  Filename:           TyrannosaurusIdleState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        State for idle.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

public class TyrannosaurusIdleState : TyrannosaurusState
{
    public TyrannosaurusIdleState(TyrannosaurusController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.Agent.destination = context.transform.position;
        //context.Agent.isStopped = true;
        context.Idle();
    }

    public override void OnStateExit()
    {
        //context.Agent.isStopped = false;
    }

    public override void OnStateRun()
    {
        if (context.CurrentHealth == 0)
        {
            context.ChangeState(context.DieState);
        }
        else if (context.PlayerInRange())
        {
            context.ChangeState(context.ChargeState);
        }
        else
        {
            context.FaceTarget();
        }
    }
}
