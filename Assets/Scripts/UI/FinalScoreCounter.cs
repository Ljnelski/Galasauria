/*  Filename:           FinalScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Automatically show the animated score in the UI
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class FinalScoreCounter : AnimatedDigitGroup
{
    void OnEnable()
    {
        // TODO, We may have to find the specific score counter for current player when we go multiplayer
        ScoreCounter scoreCounter = FindObjectOfType<ScoreCounter>();
        Debug.Log(scoreCounter.CurrentScore);
        if (scoreCounter != null)
        {
            Increase(scoreCounter.CurrentScore);
        }
    }
}
