/*  Filename:           GameDifficultyManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Interact with ToggleGameDifficulty.cs to store the difficulty settings
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 26, 2022 (Yuk Yee Wong): Set default difficulty to easy.
 *  
 */

using UnityEngine;

public class GameDifficultyManager : MonoBehaviour
{
    public static GameDifficultyManager Instance;
    public GameEnums.GameDifficulty Difficulty { get; private set; }

    private void Awake()
    {
        // Set default difficulty level
        SetDifficulty(GameEnums.GameDifficulty.EASY);

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
