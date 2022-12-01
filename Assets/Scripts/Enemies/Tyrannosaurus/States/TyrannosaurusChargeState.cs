/*  Filename:           TyrannosaurusChargeState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        State for charing player.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class TyrannosaurusChargeState : TyrannosaurusState
{
    private bool _charging;

    public TyrannosaurusChargeState(TyrannosaurusController context) : base(context)
    {
        ;
    }

    public override void OnStateEnter()
    {
        context.Agent.destination = context.ChargeTarget.position;
        context.Charge();

        _charging = true;

        if (context.Timers)
        {
            context.Timers.CreateTimer(context.ChargeInterval / 1000f, () => { 
                _charging = false;
                Debug.Log($"{context.ChargeInterval} reached");
            });
        }
    }

    public override void OnStateExit()
    {
        ;
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
