using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class NetworkUpgrade<TUpgradeData> : ScriptableObject where TUpgradeData : ScriptableObject
{
    [SerializeField] public TUpgradeData Data { get; private set; }
    [SerializeField] protected List<RecipeIngredient> _inputItems;
    protected NetworkPlayerController _networkPlayerController;

    public void DoUpgrade(NetworkPlayerController networkPlayerController)
    {
        if (!networkPlayerController.Inventory.HasItems(_inputItems))
        {
            Debug.Log("Insuffucent Resources Upgrade");
            return;
        }
        _networkPlayerController = networkPlayerController;
        UpgradeLogic();
    }

    protected abstract void UpgradeLogic();
}