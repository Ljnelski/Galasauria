/*  Filename:           PlayerCamera.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 16, 2022
 *  Description:        Focus's the Cinemachine Camera to the Player
 *  Revision History:   November 3, 2022 (Liam Nelski): Initial script. *                     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
    // Start is called before the first frame update
    void Start()
    {
       camera = GetComponent<CinemachineVirtualCamera>();
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        camera.Follow = playerPos;
        camera.LookAt = playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
