/*  Filename:           NetworkPlayerDashBar.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayerDashBar : UINetworkPlayerMonoBehaviourDataReader<NetworkPlayerController>
{
    private Slider _dashCoolDownBarSlider;

    public override void SetUp(NetworkPlayerController networkPlayerController)
    {
        base.SetUp(networkPlayerController);

        if (_targetScript == null)
        {
            Debug.LogError("NetworkPlayerDashBar ERROR: HUDTargetPlayer Script does not have a valid Target");
            return;
        }

        _targetScript.OnDashCoolDownUpdated += UpdateDashBar;
        _dashCoolDownBarSlider = GetComponent<Slider>();
        UpdateDashBar();
    }

    private void UpdateDashBar()
    {
        float newValue = (_targetScript.DashCoolDownMiliseconds - _targetScript.CurrentDashCoolDown * 1000) / _targetScript.DashCoolDownMiliseconds;
        _dashCoolDownBarSlider.value = newValue;
    }
}