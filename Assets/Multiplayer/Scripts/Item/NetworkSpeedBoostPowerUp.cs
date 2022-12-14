/*  Filename:           NetworkSpeedBoostPowerUp.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class NetworkSpeedBoostPowerUp : NetworkUseableItem
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float boostTime;

    public override void ItemEffect()
    {
        // Remove Item from inventory
        _targetNetworkPlayerController.Inventory.RemoveItem(_itemData);

        // Multiply Player Speed
        _targetNetworkPlayerController.CurrentSpeed = _targetNetworkPlayerController.BaseSpeed * speedMultiplier;

        // Create Action Timer to reset player Speed
        _targetNetworkPlayerController.BoostSpeed(boostTime);
    }
}
