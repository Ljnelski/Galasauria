/*  Filename:           Interactor.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        June 29, 2023
 *  Description:        Chooses What Interactable Object In the world To Interact with
 *  Revision History:   June 29, 2022 (Liam Nelski): Initial script.
 *                      July 07, 2023 (Liam Nelski): Added Interator value and inventory to interactor script. can differentiate what input the player presses
 *                      July 08, 2023 (Liam Nelski): Added Check to see if Interactor can be interacted with
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private float _findClosestInteractableRate;

    private IInteractable _targetInteractable;
    private List<IInteractable> _interactables;

    private void Awake()
    {
        _interactables = new List<IInteractable>();
    }    

    public void Interact(InteractValue value, Inventory inventory)
    {
        if (_targetInteractable != null)
        {
            _targetInteractable.Interact(value, inventory);
        }
    }

    private IEnumerator SearchAndTargetClosestInteractable()
    {
        while (_interactables.Count >= 1)
        {
            TargetClosestInteractable();
            yield return new WaitForSeconds(_findClosestInteractableRate);
        }
    }

    private void TargetClosestInteractable()
    {
        IInteractable closestInteractable = GetClosestInteractable();

        if (closestInteractable == _targetInteractable) return;

        if (closestInteractable != null)
        {
            _targetInteractable.Detarget();
            _targetInteractable = closestInteractable;
            _targetInteractable.Target();
        }
    }

    private IInteractable GetClosestInteractable()
    {
        IInteractable closestTarget = null;
        
        foreach (IInteractable interactable in _interactables)
        {
            // If there is no closest target currently then it is the closet target
            if(closestTarget == null)
            {
                closestTarget = interactable;
                continue;
            }

            float distance = Vector3.Distance(transform.position, interactable.WorldPosition);
            float currentClosestDistance = Vector3.Distance(transform.position, closestTarget.WorldPosition);

            if (distance < currentClosestDistance)
            {
                closestTarget = interactable;
            }

        }

        return closestTarget;
    }
    private void OnTriggerEnter(Collider other)
    {
        IInteractable newInteractable;

        if (other.gameObject.TryGetComponent<IInteractable>(out newInteractable))
        {
            // Check if the Interactor can be Interacted with
            if (newInteractable.IsInteractable) return;

            // Check if the new Interactable is already in the list before adding to avoid duplicates (Shouldn't happen but just to be safe)
            if (!_interactables.Contains(newInteractable))
            {
                _interactables.Add(newInteractable);
            }

            // If its the only interactable in the list then make the new Interactable the target
            if (_interactables.Count == 1)
            {
                _targetInteractable = newInteractable;
                _targetInteractable.Target();
            }
            else if(_interactables.Count > 1) // If there is more than one interactable in the list, start the coroutine that finds the closest and target it
            {
                StartCoroutine(SearchAndTargetClosestInteractable());
            }
        }
        else
        {
            Debug.Log("Interactor ERROR: GameObject with interactable layer does not have a Script with a IInteractable interface");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactableExiting;

        // Does Exiting Object Have a Interactable
        if (!other.TryGetComponent<IInteractable>(out interactableExiting)) return;

        // Is Exiting Object in the list of Interactables
        if (!_interactables.Contains(interactableExiting)) return;

        _interactables.Remove(interactableExiting);

        // If the interactable is the current target detarget it and look for another
        if (interactableExiting == _targetInteractable)
        {
            _targetInteractable.Detarget();
            _targetInteractable = null;
        }

        // See if there is another one in range and switch to it as the targeted interactor
        TargetClosestInteractable();
    }

    

    private void OnDrawGizmos()
    {
        if (_interactables != null && _interactables.Count > 0)
        {
            Gizmos.color = Color.blue;
            foreach (var interactable in _interactables)
            {
                Gizmos.DrawLine(transform.position, interactable.WorldPosition);
                Gizmos.DrawWireSphere(interactable.WorldPosition, 0.5f);
            }
        }
        if(_targetInteractable != null )
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_targetInteractable.WorldPosition, 0.55f);
        }
    }
}
