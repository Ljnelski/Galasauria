/*  Filename:           HUDTargetPlayer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13th, 2022
 *  Description:        Provides a central location for UI Elements to read from the GameObject the UI is Attached to
 *  Revision History:   November 13th (Liam Nelski): Inital Script.
 *                      
 */
using UnityEngine;

public class HUDTargetPlayer : MonoBehaviour
{
    public GameObject TargetPlayer { get; private set; }
    public bool ValidTarget { get; private set; } = true;

    private void Awake()
    {
        TargetPlayer = GameObject.FindGameObjectWithTag("Player");

        if (TargetPlayer == null)
        {
            Debug.LogError("HUDTargetPlayer ERROR: Cannot find player, HUD will not update");
            ValidTarget = false;
        }
    }

}
