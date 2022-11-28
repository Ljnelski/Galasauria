/*  Filename:           RaptorState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 27, 2022
 *  Description:        Abstract raptor state
 *  Revision History:   November 27 (Yuk Yee Wong): Initial script.
 */

public abstract class RaptorState : BaseState<RaptorController>
{
    protected RaptorState(RaptorController context) : base(context)
    {
    }
}
