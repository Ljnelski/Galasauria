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

    protected virtual void OnEnable()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }

        BeforeAddingListener();

        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    protected void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    protected virtual void BeforeAddingListener() { }

    protected abstract void OnToggleValueChanged(bool value);
}
