/*  Filename:           SpiderChaseState.cs
 *  Author:             Liam Nelski (301064116), Yuk Yee Wong (301234795)
 *  Last Update:        October 10th, 2022
 *  Description:        State For Chasing Player
 *  Revision History:   November 9th (Liam Nelski): Inital Script.
 *                      November 25th (Yuk Yee Wong): Added animation and logic to change to attack state.
 */

using UnityEngine;

public class SpiderChaseState : SpiderState
{
    public SpiderChaseState(SpiderController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Walk();
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        context.Agent.destination = context.ChaseTarget.position;

        if (!context.PlayerInRange())
        {
            context.ChangeState(context.IdleState);
        }
        else if (context.ReachedPlayer())
        {
            context.ChangeState(context.AttackState);
        }
    }
}

