/*  Filename:           SpiderIdleState.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        abstract Spider State
 *  Revision History:   November 9th (Liam Nelski): Inital Script.
 */
public class SpiderIdleState : SpiderState
{
    public SpiderIdleState(SpiderController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Agent.isStopped = true;
    }

    public override void OnStateExit()
    {
        context.Agent.isStopped = false;
    }

    public override void OnStateRun()
    {
        if(context.PlayerInRange())
        {
            context.ChangeState(context.ChaseState);
        }
    }
}

