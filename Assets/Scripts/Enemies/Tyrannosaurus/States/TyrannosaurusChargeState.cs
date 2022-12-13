/*  Filename:           TyrannosaurusChargeState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 5, 2022
 *  Description:        State for charing player.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 *                      December 5, 2022 (Yuk Yee Wong): Added enable/disable destroyer
 */

using UnityEngine;

public class TyrannosaurusChargeState : TyrannosaurusState
{
    private bool _charging;
    private bool _exited;

    public TyrannosaurusChargeState(TyrannosaurusController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Agent.destination = context.ChargeTarget.position;
        context.Run();
        context.EnableDestroyer(true);
        _charging = true;

        if (context.Timers)
        {
            context.Timers.CreateTimer(context.ChargeInterval / 1000f, () => { 
                _charging = false;
                if (!_exited)
                {
                    context.EnableDestroyer(false);
                }
                // Debug.Log($"{context.ChargeInterval} reached");
            });
        }
    }

    public override void OnStateExit()
    {
        _exited = true;
        context.EnableDestroyer(false);
    }

    public override void OnStateRun()
    {
        if (context.CurrentHealth == 0)
        {
            context.ChangeState(context.DieState);
        }
        else if (!_charging || context.ReachedPlayer() || context.ReachedDestination())
        {
            context.ChangeState(context.AttackState);
        }
    }
}
