/*  Filename:           PlasmaCasterUpgrade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 16, 2022
 *  Description:        Data for plasmaCaster upgrades
 *  Revision History:   December 4rd, 2022 (Liam Nelsik): Inital Script
 */
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Upgrades/PlasmaCasterUpgrade", fileName = "PlasmaCasterUpgrade")]
public class PlasmaCasterUpgrade : Upgrade<PlasmaCasterContext>
{
    [field:SerializeField] public float DamageChange { get; set; }
    [field:SerializeField] public float FireRateChange { get; set; }
    [field:SerializeField] public float RangeChange { get; set; }
    [field:SerializeField] public float ShotCountChange { get; set; }

    protected override void UpgradeLogic()
    {
        _playerController.equipables[1].GetComponent<PlasmaCaster>().Upgrade(this);
    }
}
