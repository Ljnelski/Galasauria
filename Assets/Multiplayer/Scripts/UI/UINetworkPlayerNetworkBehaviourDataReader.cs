using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class UINetworkPlayerNetworkBehaviourDataReader<TTargetScript> : NetworkBehaviour where TTargetScript : NetworkBehaviour
{
    protected TTargetScript _targetScript;
    public virtual void SetUp(TTargetScript targetScript)
    {
        _targetScript = targetScript;
    }
}
