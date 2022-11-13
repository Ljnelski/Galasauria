/*  Filename:           HUDTargetPlayer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13th, 2022
 *  Description:        Timer that has a Callback for a void 
 *  Revision History:   November 13th (Liam Nelski): Inital Script.
 *                      
 */
using UnityEngine;

public class HUDTargetPlayer : MonoBehaviour
{
    public GameObject TargetPlayer { get; private set; }
    public bool ValidTarget { get; private set; }

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
