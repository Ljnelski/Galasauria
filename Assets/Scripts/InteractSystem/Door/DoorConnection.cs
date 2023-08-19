/*  Filename:           DoorConnection.cs
 *  Author:             Liam Nelski
 *  Last Update:        Augest 8, 2022
 *  Description:        Acts a connection for a door and a button so it can be opened when interacted with
 *  Revision History:   Augest 8, 2022 (Liam Nelski): Initial script.
 */
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactables/DoorConnection", fileName ="DoorConnection_ID")]
public class DoorConnection : ScriptableObject
{
    public Action Open;
}
