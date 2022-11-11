/*  Filename:           SpiderChaseState.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        State For Chasing Player
 *  Revision History:   November 9th (Liam Nelski): Inital Script.
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
        ;
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        context.Agent.destination = context.playerTransform.position;

        if(!context.PlayerInRange())
        {
            context.ChangeState(context.IdleState);
        }
    }
}

