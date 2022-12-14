using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UINetworkPlayerMonoBehaviourDataReader<TTargetScript> : MonoBehaviour where TTargetScript : MonoBehaviour
{
    protected TTargetScript _targetScript;
    public virtual void SetUp(TTargetScript targetScript)
    {
        _targetScript = targetScript;
    }
}
