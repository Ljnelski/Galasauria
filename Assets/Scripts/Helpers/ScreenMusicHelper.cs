/*  Filename:           ScreenMusicHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Attached to screen to play different music on enable and disable
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using UnityEngine;

public class ScreenMusicHelper : MonoBehaviour
{
    [SerializeField] private GameEnums.Screen screen;
    [SerializeField] private bool triggerDisable = true;

    private void OnEnable()
    {
        StartCoroutine(waitForManager());
    }

    private void OnDisable()
    {
        if (triggerDisable)
        {
            SoundManager.Instance.CloseScreen();
        }
    }

    private IEnumerator waitForManager()
    {
        if (SoundManager.Instance == null)
        {
            yield return new WaitForEndOfFrame();
        }

        SoundManager.Instance.OpenScreen(screen);
    }
}
