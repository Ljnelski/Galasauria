/*  Filename:           UIPlayerDataReaderScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        Base screen class for any scripts that translate data from player to UI
 *  Revision History:   November 25, 2022 (Yuk Yee Wong): Initial script. 
 *                      
 */

using UnityEngine;

/// <summary>
/// Inherit BaseScreen class instead of Monbehaviour
/// </summary>
/// <typeparam name="TTargetScript"></typeparam>
public class UIPlayerDataReaderScreen<TTargetScript> : BaseScreen where TTargetScript : MonoBehaviour
{
    protected TTargetScript _targetScript;
    protected void GetTargetScript()
    {
        GameObject playerData = GameObject.Find("Player");

        if (playerData == null)
        {
            Debug.LogError("PlayerReader ERROR: Could Not find Player In Scence, Add player Prefab to screen");
            return;
        }

        _targetScript = playerData.GetComponent<TTargetScript>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_targetScript == null)
        {
            GetTargetScript();
        }
    }
}
