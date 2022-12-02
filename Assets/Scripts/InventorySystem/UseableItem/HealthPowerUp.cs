using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : UseableItem
{
    [SerializeField] private float healthAmount;
    public override void ItemEffect()
    {
        _targetPlayerController.Inventory.RemoveItem(_itemData);
        _targetPlayerController.CurrentHealth =
            Mathf.Min(_targetPlayerController.CurrentHealth +
            healthAmount, _targetPlayerController.MaxHealth);
    }
}
