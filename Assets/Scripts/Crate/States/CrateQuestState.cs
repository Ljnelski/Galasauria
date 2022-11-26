using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateQuestState : CrateState
{
    public CrateQuestState(CrateController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.OpenQuestInterface();
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        if (context.Unlocked || context.CurrentHealth == 0)
        {
            context.ChangeState(context.UnlockState);
        }
        else if (!context.IsInventoryOwnerNear())
        {
            context.ClearInventoryOwner();
            context.ChangeState(context.LockState);
        }
    }
}
