/*  Filename:           BaseController.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 10th, 2022
 *  Description:        Base State Class for Player
 *  Revision History:   November 10th, 2022 (Liam Nelski): Initial script, Moved from PlayerController.cs. *                     
 */
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