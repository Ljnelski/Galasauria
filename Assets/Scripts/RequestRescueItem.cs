using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestRescueItem : MonoBehaviour
{
    [SerializeField] private GameEndScreen screen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameFinisher.enabledRequestRescue += 1;
            if (GameFinisher.enabledRequestRescue >= 5)
            {
                screen.Open(true);
            }
            else
            {
                Debug.Log(GameFinisher.enabledRequestRescue);
                Destroy(gameObject);
            }
        }
    }
}
