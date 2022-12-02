/*  Filename:           UIPlayerDataReader.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13, 2022
 *  Description:        Base class for any scripts that translate data from player to UI
 *  Revision History:   November 13, 2022 (Liam Nelski): Initial script. 
 *                      
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerDataReader<TTargetScript> : MonoBehaviour where TTargetScript : MonoBehaviour
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
}
