/*  Filename:           ScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 8, 2022
 *  Description:        Contains public function to increase the score displayed on the UI
 *  Revision History:   November 8, 2022 (Yuk Yee Wong): Initial script.
 *                      November 12, 2022 (Yuk Yee Wong): Assign the inventoryIncrementAction
 */

using System.Collections;
using UnityEngine;

public class ScoreCounter : AnimatedDigitGroup
{
    public int CurrentScore { get { return CurrentNumber; } }

    void Awake()
    {
        StartCoroutine(WaitForManagerInstance());
    }

    private IEnumerator WaitForManagerInstance()
    {
        if (InventoryManager.Instance == null || InventoryManager.Instance.Inventories.Count == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        InventoryManager.Instance.Inventories[0].inventoryIncrementAction += IncreaseItemScore;
    }

    private void IncreaseItemScore(ItemData itemData)
    {
        Increase(itemData.score);
    }
}
