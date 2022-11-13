/*  Filename:           ScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 8, 2022
 *  Description:        Contains public function to increase the score displayed on the UI
 *  Revision History:   November 8, 2022 (Yuk Yee Wong): Initial script.
 *                      November 12, 2022 (Yuk Yee Wong): Assign the inventoryIncrementAction
 *                      Novemner 13th, 2022 (Liam Nelski): Remove dependence on InventorySystem
 */

using System.Collections;
using UnityEngine;

public class ScoreCounter : AnimatedDigitGroup<Inventory>
{
    public int CurrentScore { get { return CurrentNumber; } }

    void Start()
    {
        GetTargetScript();
        _targetScript.inventoryIncrementAction += IncreaseItemScore;
    }

    private void IncreaseItemScore(ItemData itemData)
    {
        Increase(itemData.score);
    }
}
