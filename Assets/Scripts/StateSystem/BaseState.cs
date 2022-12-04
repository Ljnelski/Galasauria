/*  Filename:           BaseState.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        Abstract Class for all States
 *  Revision History:   October 10th, 2022 (Liam Nelski): Initial script.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<TControllerContext> where TControllerContext : BaseController<TControllerContext>
{
    protected TControllerContext context;
    public BaseState(TControllerContext context)
    {
        this.context = context;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateRun();
    public abstract void OnStateExit();
}
