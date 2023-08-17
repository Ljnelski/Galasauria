/*  Filename:           PLayerDetector.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        July 22, 2022
 *  Description:        Detects if the player enters or exits on a trigger
 *  Revision History:   July 22, 2023 (Liam Nelski): Inital Script.
 */
using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Action<PlayerController> PlayerEntered;
    public Action<PlayerController> PlayerExited;

    [SerializeField] private float _detectionRadius = 1.0f;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = _detectionRadius;

        if (GetComponent<SphereCollider>().isTrigger == false)
        {
            Debug.Log("PlayerDetector ERROR: The dcollider for detecting the player must be a trigger");
        }

    }
    public void SetDetectionRadius(float detectionRadius)
    {
        GetComponent<SphereCollider>().radius = _detectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerEntered?.Invoke(player);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerExited?.Invoke(player);
        }
    }
}
