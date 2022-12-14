/*  Filename:           BaseNetworkController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using Unity.Netcode;

public abstract class BaseNetworkController<TStateContext> : NetworkBehaviour where TStateContext : BaseNetworkController<TStateContext>
{
    protected BaseNetworkState<TStateContext> activeState;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (activeState != null)
        {
            activeState.OnStateRun();
        }
    }

    protected void ChangeState(BaseNetworkState<TStateContext> newState)
    {
        if (activeState != null)
        {
            activeState.OnStateExit();
        }

        activeState = newState;

        activeState.OnStateEnter();
    }
}