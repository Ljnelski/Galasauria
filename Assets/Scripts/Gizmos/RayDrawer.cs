/*  Filename:           LineDrawer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 3, 2022
 *  Description:        Draws a Gizmos RayCase from start to finish, For testing
 *  Revision History:   Novemeber 3, 2022 (Liam Nelski): Initial script. *                     
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDrawer : MonoBehaviour
{
    public Transform startPos;
    public Vector3 rayDir;
    public Color drawColour = Color.red;

    public float RayLength;

    public bool active;

    private void OnDrawGizmos()
    {
        if (active)
        {
            Gizmos.color = drawColour;
            Gizmos.DrawRay(startPos.position, rayDir * RayLength);           
        }

    }
}
