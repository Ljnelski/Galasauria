/*  Filename:           ActionTimer.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 12th, 2022
 *  Description:        Timer that has a Callback for a void 
 *  Revision History:   November 12th (Liam Nelski): Inital Script.
 *                      Novmeber 13th (Liam Nelski): Added Option Callback for onTick
 *                      
 */

using System;
public class ActionTimer
{
    private Action _onCompleteCallback;
    private Action<ActionTimer> _OnDestroyCallback;
    private Action<float> _onTickCallback;
    private float _timerTime;

    public ActionTimer(Action<ActionTimer> onDestroy)
    {
        _OnDestroyCallback = onDestroy;
    }

    public void StartTimer(float timer, Action completeCallback, Action<float> onTickCallback)
    {
        _timerTime = timer;
        _onCompleteCallback += completeCallback;
        _onTickCallback += onTickCallback;
    }

    public void Tick(float deltaTime)
    {
        _timerTime -= deltaTime;
        _onTickCallback?.Invoke(_timerTime);

        if (_timerTime < 0)
        {
            Complete();
        }
    }

    public void Complete()
    {
        _onCompleteCallback.Invoke();
        _onCompleteCallback = null;
        _onTickCallback = null;
        _OnDestroyCallback.Invoke(this);

    }
}

