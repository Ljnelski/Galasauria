/*  Filename:           UINetworkPlayerNetworkBehaviourDataReader.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using Unity.Netcode;

public abstract class UINetworkPlayerNetworkBehaviourDataReader<TTargetScript> : NetworkBehaviour where TTargetScript : NetworkBehaviour
{
    protected TTargetScript _targetScript;
    public virtual void SetUp(TTargetScript targetScript)
    {
        _targetScript = targetScript;
    }
}
