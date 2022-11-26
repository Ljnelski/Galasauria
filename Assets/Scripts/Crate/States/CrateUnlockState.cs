using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUnlockState : CrateState
{
    public CrateUnlockState(CrateController context) : base(context)
    {
    }

    public override void OnStateEnter()
    {
        context.OpenCrate();
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        ;
    }
}
