using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<StateController> where StateController : MonoBehaviour
{
    public StateController ControllerScript;
    BaseState(StateController controllerScript)
    {
        ControllerScript = controllerScript;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateRun();
    public abstract void OnStateExit();

    
}
