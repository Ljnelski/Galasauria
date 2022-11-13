/*  Filename:           TimerPool.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 12th, 2022
 *  Description:        Script that can contain n number of Action Timers that can be placed on objects
 *  Revision History:   November 12th (Liam Nelski): Inital Script.
 *                      
 */
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerPool : MonoBehaviour
{
    Queue<ActionTimer> _pooledTimers;
    List<ActionTimer> _activeTimers;

    private void Awake()
    {
        _pooledTimers= new Queue<ActionTimer>();
        _activeTimers= new List<ActionTimer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(_activeTimers.Count > 0)
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < _activeTimers.Count; i++)
            {
                _activeTimers[i].Tick(deltaTime);
            }
        }
    }

    public void CreateTimer(float duration, Action callback)
    {
        ActionTimer newTimer;
        if (_pooledTimers.Count > 0) {
            newTimer = _pooledTimers.Dequeue();
            newTimer.StartTimer(duration, callback);
        }
        else
        {
            newTimer = new ActionTimer(PoolTimer);
            newTimer.StartTimer(duration, callback);
        }

        _activeTimers.Add(newTimer);
    }

    protected void PoolTimer(ActionTimer newTimer)
    {
        _activeTimers.Remove(newTimer);
        _pooledTimers.Enqueue(newTimer);
    }
}
