/*  Filename:           IInteractable.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        July 07, 2023
 *  Description:        Chooses What Interactable Object In the world To Interact with
 *  Revision History:   July 07, 2022 (Liam Nelski): Initial script.
 */
using UnityEngine;

public interface IInteractable
{
    public Vector3 WorldPosition { get; }
    public bool IsInteractable { get; }
    public void Interact(InteractValue value, Inventory inventory);
    public void Target();
    public void Detarget();
}
