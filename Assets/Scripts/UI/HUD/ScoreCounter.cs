/*  Filename:           ScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795), Liam Nelski (301064116)
 *  Last Update:        November 25th, 2022
 *  Description:        Contains public function to increase the score displayed on the UI
 *  Revision History:   November 8, 2022 (Yuk Yee Wong): Initial script.
 *                      November 12, 2022 (Yuk Yee Wong): Assign the inventoryIncrementAction
 *                      November 13th, 2022 (Liam Nelski): Remove dependence on InventorySystem
 *                      November 25th, 2022 (Yuk Yee Wong): Clean the script to remove the unnecessary CurrentScore
 */

public class ScoreCounter : AnimatedDigitGroup<PlayerController>
{
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