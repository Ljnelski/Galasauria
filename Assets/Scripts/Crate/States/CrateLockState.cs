using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateLockState : CrateState
{
    public CrateLockState(CrateController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.CloseQuestInterface();
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        if (context.IsInventoryOwnerNear())
        {
            context.ChangeState(context.QuestState);
        }
        else if (context.Unlocked || context.CurrentHealth == 0)
        {
            context.ChangeState(context.UnlockState);
        }
    }
}
