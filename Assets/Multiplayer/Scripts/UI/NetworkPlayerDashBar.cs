using System.Collections;
using System.Collections.Generic;
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