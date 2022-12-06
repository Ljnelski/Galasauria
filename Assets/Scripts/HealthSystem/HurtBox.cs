using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class HurtBox : MonoBehaviour
{
    [SerializeField] private string _targetTag;

    public Action OnOnTriggerEnter;
    public Action OnOnTriggerEnterTargetLayer;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On trigger, Tag: " + other.tag + "\n Target Tag: " + _targetTag);
        if (other.tag == _targetTag)
        {
            OnOnTriggerEnterTargetLayer?.Invoke();
            OnOnTriggerEnter?.Invoke();
        }
        else
        {
            OnOnTriggerEnter?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnOnTriggerEnterTargetLayer = null;
        OnOnTriggerEnter = null;
    }
}
