/*  Filename:           CrateLockState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        State for locked crate.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

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
