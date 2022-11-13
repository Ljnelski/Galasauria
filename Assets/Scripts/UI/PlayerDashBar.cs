using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashBar : MonoBehaviour
{
    PlayerController _targetPlayerController;
    private Slider _dashCoolDownBarSlider;

    // Start is called before the first frame update
    void Start()
    {
        HUDTargetPlayer playerData = GetComponentInParent<HUDTargetPlayer>();

        if (playerData == null)
        {
            Debug.LogError("PlayerHealthBar ERROR: Could not find HUDTargetPlayer, Add HUDTargetPlayer to this UI elements Parent");
            return;
        }

        if (playerData.ValidTarget)
        {
            _targetPlayerController = playerData.TargetPlayer.GetComponent<PlayerController>();
            _targetPlayerController.OnDashCoolDownUpdated += UpdateDashBar;
            _dashCoolDownBarSlider = GetComponent<Slider>();

            UpdateDashBar();
        }
        else
        {
            Debug.LogError("PlayerHealthBar ERROR: HUDTargetPlayer Script does not have a valid Target");
        }
    }

    private void UpdateDashBar()
    {
        float newValue = (_targetPlayerController.DashCoolDownMiliseconds - _targetPlayerController.CurrentDashCoolDown * 1000) / _targetPlayerController.DashCoolDownMiliseconds;
        _dashCoolDownBarSlider.value = newValue;    
    }
}
