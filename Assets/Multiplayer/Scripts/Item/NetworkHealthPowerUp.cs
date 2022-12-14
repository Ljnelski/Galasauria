using System.Collections;
using System.Collections.Generic;
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
