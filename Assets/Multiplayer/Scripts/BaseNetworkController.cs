using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using UnityEngine;

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