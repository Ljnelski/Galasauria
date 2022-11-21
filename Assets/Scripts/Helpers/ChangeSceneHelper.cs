/*  Filename:           ChangeSceneHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Attached to game object like button to initiate the load scene function on click; not required component because user can add the event by a tailor-made sequence
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 13, 2022 (Yuk Yee Wong): Add reload current scene option.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneHelper : MonoBehaviour
{
    [SerializeField] private bool reloadCurrentScene;
    [Header("Scene Index will be ignored if reload current scene is checked")]
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
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
