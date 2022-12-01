/*  Filename:           FinalScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795), Liam Nelski (301064116)
 *  Last Update:        November 25, 2022
 *  Description:        Automatically show the animated score in the UI
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 *                      November 13th, 2022 (Liam Nelski): Updated to grab data from player Controller
 *                      November 25th, 2022 (Yuk Yee Wong): Fix the null reference for _targetScript when running OnEnable
 */

using UnityEngine;

public class FinalScoreCounter : AnimatedDigitGroup<PlayerController>
{
    void OnEnable()
    {
        if (_targetScript == null)
        {
            GetTargetScript();
        }

        DrawNumber(_targetScript.CurrentScore);
    }
}
