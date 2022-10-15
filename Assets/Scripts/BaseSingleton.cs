using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set;}

    protected virtual void Awake()
    {
        if(Instance == null)
        {
            Instance = (T)FindObjectOfType(typeof(T));
        } else
        {
            Destroy(gameObject);
        }
    }
   
}
