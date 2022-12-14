using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPowerUp : UseableItem
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float boostTime;

    public override void ItemEffect()
    {
        // Remove Item from inventory
        _targetPlayerController.Inventory.RemoveItem(_itemData);

        // Multiply Player Speed
        _targetPlayerController.CurrentSpeed = _targetPlayerController.BaseSpeed * speedMultiplier;

        // Create Action Timer to reset player Speed
        _targetPlayerController.Timers.CreateTimer(boostTime / 1000f, () =>
        {
            _targetPlayerController.CurrentSpeed = _targetPlayerController.BaseSpeed;
        });        
    }
}
