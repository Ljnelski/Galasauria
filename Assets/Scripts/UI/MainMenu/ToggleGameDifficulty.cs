/*  Filename:           ToggleGameDifficulty.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Used in options screen to toggle different difficulty and store in game difficulty manager
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGameDifficulty : ToggleBase
{
    [SerializeField] private GameEnums.GameDifficulty difficulty;

    protected override void BeforeAddingListener()
    {        
        toggle.isOn = difficulty.Equals(GameDifficultyManager.Instance.Difficulty);
    }

    protected override void OnToggleValueChanged(bool value)
    {
        if (value)
        {
            GameDifficultyManager.Instance.SetDifficulty(difficulty);
        }
    }
}
