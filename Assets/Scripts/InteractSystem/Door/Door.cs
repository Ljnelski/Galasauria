/*  Filename:           Door.cs
 *  Author:             Liam Nelski
 *  Last Update:        Augest 8, 2022
 *  Description:        Holds Logic that opens Door
 *  Revision History:   Augest 8, 2022 (Liam Nelski): Initial script.
 */
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorConnection doorConnection;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if(doorConnection == null) 
        {
            Debug.Log("Door ERROR: Door has no Button Connection");
        }   
    }
    void OnEnable()
    {
        doorConnection.Open += Open;
    }

    private void Open()
    {
        Debug.Log("COMMUNUICATION SUCCESFUL");
        animator.SetTrigger("OpenDoor");
    }

    private void OnDisable()
    {
        doorConnection.Open -= Open;
    }
}
