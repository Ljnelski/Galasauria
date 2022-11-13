/*  Filename:           ActionTimer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 12th, 2022
 *  Description:        Timer that has a Callback for a void 
 *  Revision History:   November 12th (Liam Nelski): Inital Script.
 *                      
 */

using System;

public class ActionTimer
{
    private Action _timerCallback;
    private Action<ActionTimer> _OnDestroy;
    private float _timerTime;

    public ActionTimer(Action<ActionTimer> onDestroy)
    {
        _OnDestroy = onDestroy;
    }

    public void StartTimer(float timer, Action callback)
    {
        _timerTime = timer;
        _timerCallback += callback;
    }

    public void Tick(float deltaTime)
    {
        _timerTime -= deltaTime;
        if (_timerTime < 0)
        {
            Complete();
        }
    }

    public void Complete()
    {
        _timerCallback.Invoke();
        _timerCallback = null;
        _OnDestroy.Invoke(this);

    }
}

