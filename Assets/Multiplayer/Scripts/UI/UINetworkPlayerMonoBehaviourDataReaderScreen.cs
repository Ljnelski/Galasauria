/*  Filename:           UINetworkPlayerMonoBehaviourDataReaderScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

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
