/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 16, 2022
 *  Description:        Data for upgrades
 *  Revision History:   October 16, 2022 (Liam Nelski): Initial script.  
 *                      December 3rd, 2022 (Liam Nelsik): Implemented
 */
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Upgrades/CyberBladeUpgrade", fileName = "CyberBladeUpgrade")]
public class CyberBladeUpgrade : Upgrade<CyberBladeContext>
{
    [field:SerializeField] public float PowerChange { get; set; }
    [field:SerializeField] public float HandelLengthChange { get; set; }
    [field:SerializeField] public float BladeLengthChange { get; set; }
    [field:SerializeField] public float SwingSpeedChange { get; set; }
    protected override void UpgradeLogic()
    {
        _playerController.equipables[0].GetComponent<CyberBlade>().Upgrade(this);
    }
}
