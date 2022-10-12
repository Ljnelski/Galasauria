/*  Filename:           ChangeSceneHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Attached to game object like button to initiate the load scene function on click; not required component because user can add the event by a tailor-made sequence
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneHelper : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private GameEnums.Screen screen;

    public void LoadScene()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ChangeScene(screen);
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
