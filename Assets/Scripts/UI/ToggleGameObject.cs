/*  Filename:           ToggleGameObject.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Used in upgrade screen to switch panel between upgradable items
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ToggleGameObject : ToggleBase
{
    [SerializeField] private GameObject affectedGameObject;

    protected override void OnToggleValueChanged(bool value)
    {
        if (affectedGameObject != null)
            affectedGameObject.SetActive(value);
    }
}

