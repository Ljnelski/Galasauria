using System.Collections;
using System.Collections.Generic;
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
