/*  Filename:           FinalScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Automatically show the animated score in the UI
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 *                      November 13th, 2022 (Liam Nelski): Updated to grab data from player Controller
 */

using UnityEngine;

public class FinalScoreCounter : AnimatedDigitGroup<PlayerController>
{
    void OnEnable()
    {
        DrawNumber(_targetScript.CurrentScore);
    }
}
