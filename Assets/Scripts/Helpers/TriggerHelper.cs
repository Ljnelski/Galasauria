/*  Filename:           TriggerHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Provides trigger events to wire 
 *  Revision History:   November 13 (Yuk Yee Wong): Inital Script.
 */

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerHelper : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvent;
    [SerializeField] private UnityEvent onTriggerExitEvent;
    [SerializeField] private string triggerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            onTriggerEnterEvent?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            onTriggerExitEvent?.Invoke();
        }
    }
}
