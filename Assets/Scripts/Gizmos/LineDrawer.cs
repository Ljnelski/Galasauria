/*  Filename:           LineDrawer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 3, 2022
 *  Description:        Draws a Gizmos line from start to finish, For testing
 *  Revision History:   Novemeber 3, 2022 (Liam Nelski): Initial script. *                     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Color drawColour = Color.red;
    private void OnDrawGizmos()
    {
        Gizmos.color = drawColour;
        Gizmos.DrawLine(startPos.position, endPos.position);
    }
}
