/*  Filename:           CrateState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        abstract Crate State
 *  Revision History:   November 26 (Yuk Yee Wong): Inital Script.
 */

public abstract class CrateState : BaseState<CrateController>
{
    protected CrateState(CrateController context) : base(context)
    {
    }
}
