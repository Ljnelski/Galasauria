using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<Context> where Context : MonoBehaviour
{
    protected Context controller;
    public BaseState(Context context)
    {
        controller = context;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateRun();
    public abstract void OnStateExit();

    
}
