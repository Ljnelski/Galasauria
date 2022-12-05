/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 16, 2022
 *  Description:        Data for upgrades
 *  Revision History:   October 16, 2022 (Liam Nelski): Initial script.  
 *                      December 3rd, 2022 (Liam Nelsik): Implemented
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade<TUpgradeData> : ScriptableObject where TUpgradeData : ScriptableObject
{
    [SerializeField] public TUpgradeData Data { get; private set; }
    [SerializeField] protected List<RecipeIngredient> _inputItems;
    protected PlayerController _playerController; 

    public void DoUpgrade(PlayerController playerController) {
        if (!playerController.Inventory.HasItems(_inputItems)) return;
        _playerController = playerController;
        UpgradeLogic();
    }

    protected abstract void UpgradeLogic();
}


[Serializable]
[CreateAssetMenu(menuName = "Items/Upgrades", fileName = "CyberBladeUpgrade")]
public class CyberBladeUpgrade : Upgrade<CyberBladeContext>
{
    protected override void UpgradeLogic()
    {
        _playerController.equipables[0].GetComponent<CyberBlade>().Upgrade(this);
    }
}

[Serializable]
[CreateAssetMenu(menuName = "Items/Upgrades", fileName = "PlasmaCasterUpgrade")]
public class PlasmaCasterUpgrade : Upgrade<PlasmaCasterContext>
{
    protected override void UpgradeLogic()
    {
        _playerController.equipables[1].GetComponent<PlasmaCaster>().Upgrade(this);
    }
}
