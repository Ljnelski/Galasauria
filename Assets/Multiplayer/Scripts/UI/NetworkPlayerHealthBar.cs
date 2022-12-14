/*  Filename:           NetworkPlayerHealthBar.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

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