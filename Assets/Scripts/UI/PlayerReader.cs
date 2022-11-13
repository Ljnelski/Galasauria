using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerReader<TTargetScript> : MonoBehaviour where TTargetScript : MonoBehaviour
{
    protected TTargetScript _targetScript;
    protected void GetTargetScript()
    {
        Debug.Log("Start UIPlayerReader");
        GameObject playerData = GameObject.Find("Player");

        if (playerData == null)
        {
            Debug.LogError("PlayerReader ERROR: Could Not find Player In Scence");
            return;
        }      
           
        _targetScript = playerData.GetComponent<TTargetScript>();
    }
}
