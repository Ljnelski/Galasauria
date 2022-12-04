/*  Filename:           ActionTimer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 21th, 2022
 *  Description:        Monoscript Wrapper for a single ActionTimer
 *  Revision History:   November 12th (Liam Nelski): Inital Script.
 *                      
 */

using System;
using System.Collections;
using UnityEngine;

public class SingleActionTimer : MonoBehaviour
{
    public ActionTimer Timer { get; private set; }
    // Use this for initialization
    public void Start()
    {
        Timer = new ActionTimer(null);
    }
    // Update is called once per frame
    void Update()
    {
        Timer.Tick(Time.deltaTime);
    }
}
