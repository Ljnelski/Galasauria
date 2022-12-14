using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NetworkPlayerHealthBar : UINetworkPlayerMonoBehaviourDataReader<NetworkPlayerController>
{
    private Slider _healthBarSlider;

    public override void SetUp(NetworkPlayerController networkPlayerController)
    {
        base.SetUp(networkPlayerController);

        if (_targetScript == null)
        {
            Debug.LogError("NetworkPlayerHealthBar ERROR: HUDTargetPlayer Script does not have a valid Target");
            return;
        }

        _targetScript.OnHealthUpdated += UpdateHealthBar;
        _healthBarSlider = GetComponent<Slider>();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float newValue = _targetScript.CurrentHealth / _targetScript.MaxHealth;
        _healthBarSlider.value = newValue;
    }

}