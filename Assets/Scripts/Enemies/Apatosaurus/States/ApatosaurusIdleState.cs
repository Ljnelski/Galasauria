/*  Filename:           ApatosaurusIdleState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        Apatosaurus Idle State
 *  Revision History:   December 12 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ApatosaurusIdleState : ApatosaurusState
{
    public ApatosaurusIdleState(ApatosaurusController context) : base(context)
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
            context.ChangeState(context.EvadeState);
        }
    }
}
