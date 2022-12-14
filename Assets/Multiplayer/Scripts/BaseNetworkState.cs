/*  Filename:           BaseNetworkState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

public abstract class BaseNetworkState<TControllerContext> where TControllerContext : BaseNetworkController<TControllerContext>
{
    protected TControllerContext context;
    public BaseNetworkState(TControllerContext context)
    {
        this.context = context;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateRun();
    public abstract void OnStateExit();
}
