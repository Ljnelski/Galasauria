/*  Filename:           ChangeSceneHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Attached to game object like button to initiate the load scene function on click; not required component because user can add the event by a tailor-made sequence
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 13, 2022 (Yuk Yee Wong): Added reload current scene option.
 *                      November 26, 2022 (Yuk Yee Wong): Added loadByDifficulty and loadByNextDifficulty.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneHelper : MonoBehaviour
{
    [Header("Priority 1: Reload Current Scene (if checked)")]
    [SerializeField] private bool reloadCurrentScene;

    [Header("Priority 2: Load by Selected Difficulty (if reload is not checked)")]
    [SerializeField] private bool loadByDifficulty;

    [Header("Priority 3: Load by Next Difficulty (if difficulty is not checked)")]
    [SerializeField] private bool loadByNextDifficulty;

    [Header("Priority 4: Load by Scene Index (if loadByDifficulty is not checked)")]
    [SerializeField] private int sceneIndex;

    [Header("Scene Type determine the type of background music to be played")]
    [SerializeField] private GameEnums.Screen screen;

    public void LoadScene()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ChangeScene(screen);
        }

        if (reloadCurrentScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (loadByDifficulty)
        {
            switch (GameDifficultyManager.Instance.Difficulty)
            {
                case GameEnums.GameDifficulty.EASY:
                    SceneManager.LoadScene(1);
                    break;
                case GameEnums.GameDifficulty.MEDIUM:
                    SceneManager.LoadScene(2);
                    break;
                case GameEnums.GameDifficulty.HARD:
                    SceneManager.LoadScene(3);
                    break;
                default:
                    Debug.LogError($"ChangeSceneHelper ERROR: {GameDifficultyManager.Instance.Difficulty} does not have a respective scene index to load, please revise the LoadScene() in ChangeSceneHelper");
                    break;
            }
        }
        else if (loadByNextDifficulty)
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    SceneManager.LoadScene(2);
                    break;
                case 2:
                    SceneManager.LoadScene(3);
                    break;
                case 3:
                    SceneManager.LoadScene(0);
                    break;
                default:
                    Debug.LogError($"ChangeSceneHelper ERROR: {GameDifficultyManager.Instance.Difficulty} does not have a respective next scene index to load, please revise the LoadScene() in ChangeSceneHelper");
                    break;
            }
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
