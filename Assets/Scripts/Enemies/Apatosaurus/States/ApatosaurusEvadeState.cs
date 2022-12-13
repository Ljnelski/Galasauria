/*  Filename:           ApatosaurusEvadeState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        State for evading player
 *  Revision History:   December 12 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ApatosaurusEvadeState : ApatosaurusState
{
    public ApatosaurusEvadeState(ApatosaurusController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.Evade();
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
    }
}
