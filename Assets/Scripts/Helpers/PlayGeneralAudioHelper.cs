/*  Filename:           PlayGeneralAudioHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Attached to game object like button to play audio function on click; not required component because user can add the event by a tailor-made sequence
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using UnityEngine;

public class PlayGeneralAudioHelper : MonoBehaviour
{
    [SerializeField] private GameEnums.GeneralAudio generalAudio;

    public void PlayAudio()
    {
        StartCoroutine(waitForManager());
    }

    private IEnumerator waitForManager()
    {
        if (SoundManager.Instance == null)
        {
            yield return new WaitForEndOfFrame();
        }

        SoundManager.Instance.PlayGeneralAudio(generalAudio);
    }
}
