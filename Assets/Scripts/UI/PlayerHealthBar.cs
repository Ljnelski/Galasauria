/*  Filename:           PlayerHealthBar.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13th, 2022
 *  Description:        Gets Health Value and puts it to UI 
 *  Revision History:   November 13th (Liam Nelski): Inital Script.
 *                      
 */
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : UIPlayerReader<PlayerController>
{
    private Slider _healthBarSlider;
    private void Start()
    {
        GetTargetScript();

        if (_targetScript == null)
        {
            Debug.LogError("PlayerHealthBar ERROR: HUDTargetPlayer Script does not have a valid Target");
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
