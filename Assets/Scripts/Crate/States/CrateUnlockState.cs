/*  Filename:           CrateUnlockState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        State for unlocked crate.
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

public class CrateUnlockState : CrateState
{
    public CrateUnlockState(CrateController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.OpenCrate();
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
