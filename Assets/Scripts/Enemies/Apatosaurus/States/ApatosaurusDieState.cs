/*  Filename:           ApatosaurusDieState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        State for dieing.
 *  Revision History:   December 12 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ApatosaurusDieState : ApatosaurusState
{
    public ApatosaurusDieState(ApatosaurusController context) : base(context)
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
