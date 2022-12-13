/*  Filename:           GameEndTrigger.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        A trigger for game end state
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class GameEndTrigger : MonoBehaviour
{
    [SerializeField] private GameEndScreen screen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            screen.Open(true);
            switch (GameDifficultyManager.Instance.Difficulty)
            {
                case GameEnums.GameDifficulty.EASY:
                    WinStatTracker.isLv1Complete = true;
                    break;
                case GameEnums.GameDifficulty.MEDIUM:
                    WinStatTracker.isLv2Complete = true;
                    break;
                case GameEnums.GameDifficulty.HARD:
                    WinStatTracker.isLv3Complete = true;
                    break;
                default:
                    Debug.LogError($"GameEndTrigger ERROR: {GameDifficultyManager.Instance.Difficulty} does not have a respective scene index");
                    break;
            }
        }
    }
}
