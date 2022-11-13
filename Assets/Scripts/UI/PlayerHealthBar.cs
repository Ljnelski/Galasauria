/*  Filename:           PlayerHealthBar.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13th, 2022
 *  Description:        Gets Health Value and puts it to UI 
 *  Revision History:   November 13th (Liam Nelski): Inital Script.
 *                      
 */
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private PlayerController _targetPlayerController;
    private Slider _healthBarSlider;
    private void Awake()
    {        
        HUDTargetPlayer playerData = GetComponentInParent<HUDTargetPlayer>();

        if (playerData == null)
        {
            Debug.LogError("PlayerHealthBar ERROR: Could not find HUDTargetPlayer, Add HUDTargetPlayer to this UI elements Parent");
            return;
        }     
       
        if(playerData.ValidTarget)
        {
            _targetPlayerController = playerData.TargetPlayer.GetComponent<PlayerController>();
            _targetPlayerController.OnHealthUpdated += UpdateHealthBar;
            _healthBarSlider = GetComponent<Slider>();

            UpdateHealthBar();
        } else
        {
            Debug.LogError("PlayerHealthBar ERROR: HUDTargetPlayer Script does not have a valid Target");
        }
        
    }
    private void UpdateHealthBar()
    {
        float newHealth = _targetPlayerController.CurrentHealth / _targetPlayerController.MaxHealth;
        _healthBarSlider.value = _targetPlayerController.CurrentHealth / _targetPlayerController.MaxHealth;
    }

}
