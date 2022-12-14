/*  Filename:           NetworkUpgrade.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

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