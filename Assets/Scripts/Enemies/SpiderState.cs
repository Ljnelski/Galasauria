/*  Filename:           SpiderState.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        abstract Spider State
 *  Revision History:   November 9th (Liam Nelski): Inital Script.
 */
public abstract class SpiderState : BaseState<SpiderController>
{
    protected SpiderState(SpiderController context) : base(context)
    {
    }
}
