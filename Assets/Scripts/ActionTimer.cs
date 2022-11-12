using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimer
{
    public bool Paused;
    public Action OnTimerComplete;

    private float _timerDuration;
    private float _timerTime;

    public ActionTimer()
    {
        Paused = true;
    }

    public void StartTimer(float timer)
    {
        Paused = false;
        _timerDuration = timer;
    }

    public void TickTimer(float timeAmount)
    {
        if(Paused)
        {
            return;
        }

        _timerTime += timeAmount;

        if(_timerTime >= _timerDuration) { }

    }

}
