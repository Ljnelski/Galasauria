/*  Filename:           HUDTargetPlayer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13th, 2022
 *  Description:        Timer that has a Callback for a void 
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
            _targetPlayerController.OnHealthUpdated += UpdateHealth;
            _healthBarSlider = GetComponent<Slider>();

            UpdateHealth();
        } else
        {
            Debug.LogError("PlayerHealthBar ERROR: HUDTargetPlayer Script does not have a valid Target");
        }
        
    }
    private void UpdateHealth()
    {
        float newHealth = _targetPlayerController.CurrentHealth / _targetPlayerController.MaxHealth;
        Debug.Log("CurrentHealth: " + _targetPlayerController.CurrentHealth);
        Debug.Log("MaxHealth: " + _targetPlayerController.MaxHealth);
        Debug.Log("Updating Health Bar, Value: " + newHealth);

        _healthBarSlider.value = _targetPlayerController.CurrentHealth / _targetPlayerController.MaxHealth;
    }

}
