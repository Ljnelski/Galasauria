/*  Filename:           WireSphereDrawer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 3, 2022
 *  Description:        Draws a Gizmos WireSphere For testing
 *  Revision History:   Novemeber 3, 2022 (Liam Nelski): Initial script. *                     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSphereDrawer : MonoBehaviour
{
    private List<WireSphere> spheres = new List<WireSphere>();

    public Color defaultColour = Color.red;
    public bool active = false;

    public void AddSphere(Vector3 position, float radius)
    {
        spheres.Add(new WireSphere(position, radius, defaultColour));
    }
    public void AddSphere(Vector3 position, float radius, Color colour)
    {
        spheres.Add(new WireSphere(position, radius, colour));
    }

    public void ClearSpheres()
    {
        spheres.Clear();
    }

    private void OnDrawGizmos()
    {      
        foreach(WireSphere sp in spheres)
        {
            Gizmos.color = sp.colour;
            Gizmos.DrawWireSphere(sp.spherePosition, sp.radius);
        }
    }    
   
    internal class WireSphere
    {
        public Vector3 spherePosition;
        public float radius;
        public Color colour;
        internal WireSphere(Vector3 spherePosition, float radius, Color colour)
        {
            this.spherePosition = spherePosition;
            this.radius = radius;
            this.colour = colour;
        }
    }
}

