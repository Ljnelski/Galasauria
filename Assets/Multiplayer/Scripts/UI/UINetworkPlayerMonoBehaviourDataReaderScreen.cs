using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit BaseScreen class instead of Monbehaviour
/// </summary>
/// <typeparam name="TTargetScript"></typeparam>
public class UINetworkPlayerMonoBehaviourDataReaderScreen<TTargetScript> : BaseScreen where TTargetScript : MonoBehaviour
{
    protected TTargetScript _targetScript;
    public virtual void SetUp(TTargetScript targetScript)
    {
        _targetScript = targetScript;
    }
}
