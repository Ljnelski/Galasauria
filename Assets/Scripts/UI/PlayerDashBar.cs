/*  Filename:           HUDTargetPlayer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13th, 2022
 *  Description:        Gets DashCoolDown value to UI
 *  Revision History:   November 13th (Liam Nelski): Inital Script.
 *                      
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashBar : UIPlayerReader<PlayerController>
{
    private Slider _dashCoolDownBarSlider;
    // Start is called before the first frame update
    private void Start()
    {
        GetTargetScript();

        if (_targetScript == null)
        {
            Debug.LogError("PlayerHealthBar ERROR: HUDTargetPlayer Script does not have a valid Target");
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
