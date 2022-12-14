using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
