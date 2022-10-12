/*  Filename:           ToggleBase.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Adds the on value changed listener to the attached toggle
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public abstract class ToggleBase : MonoBehaviour
{
    protected Toggle toggle;

    private void OnEnable()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }

        toggle.onValueChanged.AddListener(onToggleValueChanged);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(onToggleValueChanged);
    }

    protected abstract void onToggleValueChanged(bool value);
}
