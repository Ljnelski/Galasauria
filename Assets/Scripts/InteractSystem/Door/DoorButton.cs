/*  Filename:           DoorButton.cs
 *  Author:             Liam Nelski
 *  Last Update:        Augest 8, 2022
 *  Description:        Holds Logic that for a button that opens a door
 *  Revision History:   Augest 8, 2022 (Liam Nelski): Initial script.
 */
using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorConnection doorConnection;
    public Vector3 WorldPosition { get => transform.position; }

    public bool IsInteractable => _pressed;

    private bool _pressed;

    private void Start()
    {
        if(doorConnection == null)
        {
            Debug.LogError("DoorButton ERROR: DoorButton has no connection");
        }
    }

    public void Detarget()
    {
        // Show indication that button cannot be pressed
        throw new System.NotImplementedException();
    }

    public void Interact(InteractValue value, Inventory inventory)
    {
        doorConnection.Open();
        _pressed = true;
    }

    public void Target()
    {
        // Show indication that button can be pressed
        throw new System.NotImplementedException();
    }
}
