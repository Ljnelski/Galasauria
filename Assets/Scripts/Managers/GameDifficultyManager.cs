/*  Filename:           GameDifficultyManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Interact with ToggleGameDifficulty.cs to store the difficulty settings
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class GameDifficultyManager : MonoBehaviour
{
    public static GameDifficultyManager Instance;
    public GameEnums.GameDifficulty Difficulty { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetDifficulty(GameEnums.GameDifficulty difficulty)
    {
        this.Difficulty = difficulty;
    }
}
