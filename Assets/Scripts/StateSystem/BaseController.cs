using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController<TStateContext> : MonoBehaviour where TStateContext : BaseController<TStateContext>
{
    protected BaseState<TStateContext> activeState;
    // Update is called once per frame
    private void FixedUpdate()
    {
        activeState.OnStateRun();
    }

    public void ChangeState(BaseState<TStateContext> newState)
    {
        activeState.OnStateExit();

        activeState = newState;

        activeState.OnStateEnter();
    }
}