/*  Filename:           NetworkHealthPowerUp.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class NetworkHealthPowerUp : NetworkUseableItem
{
    [SerializeField] private float healthAmount;
    public override void ItemEffect()
    {
        _targetNetworkPlayerController.Inventory.RemoveItem(_itemData);
        _targetNetworkPlayerController.CurrentHealth =
            Mathf.Min(_targetNetworkPlayerController.CurrentHealth +
            healthAmount, _targetNetworkPlayerController.MaxHealth);
    }
}
