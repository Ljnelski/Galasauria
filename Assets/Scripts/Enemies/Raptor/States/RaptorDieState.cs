/*  Filename:           RaptorDieState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 27, 2022
 *  Description:        State for dieing.
 *  Revision History:   November 27, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class RaptorDieState : RaptorState
{
    public RaptorDieState(RaptorController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.Agent.isStopped = true;
        context.Die();
        DestroyCountDown();
    }

    private void DestroyCountDown()
    {
        if (context.Timers)
        {
            context.Timers.CreateTimer(context.DieInterval / 1000f, () => { context.SelfDestroy(); });
        }
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        ;
    }
}
