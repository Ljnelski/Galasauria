/*  Filename:           ToggleAudioSettings.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Used in options screen to toggle audio on and off by calling the sound manager
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ToggleAudioSettings : ToggleBase
{
    [SerializeField] private GameEnums.SoundType soundType;

    protected override void OnToggleValueChanged(bool value)
    {
        SoundManager.Instance.SetUnmuted(soundType, value);
    }
}
