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

public class ScoreCounter : AnimatedDigitGroup<PlayerController>
{
    public int CurrentScore { get { return CurrentNumber; } }
    protected override void Start()
    {
        base.Start();
        _targetScript.OnScoreIncremented += IncreaseItemScore;
    }
    private void IncreaseItemScore(int increase)
    {
        DrawNumber(increase);
    }
}