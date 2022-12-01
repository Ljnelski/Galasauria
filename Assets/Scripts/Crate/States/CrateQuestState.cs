/*  Filename:           CrateQuestState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        State for quest crate.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */


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
