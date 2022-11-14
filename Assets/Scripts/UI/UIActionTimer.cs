/*  Filename:           UIActionTimer.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Gate Control Timer 
 *  Revision History:   November 13 (Yuk Yee Wong): Inital Script.
 */

using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIActionTimer : MonoBehaviour
{
    [SerializeField] private TextMeshPro timerLabel;
    [SerializeField] private float duration;
    [SerializeField] private bool entered;
    [SerializeField] private UnityEvent onTriggerEnterEvent;
    [SerializeField] private UnityEvent oCountDownCompleteEvent;

    private bool countingDown;
    private string beforeCountDown;
    private float timePassed;

    private void Start()
    {
        beforeCountDown = string.Format("00:{0:D2}", (int)Mathf.Floor(duration));
        timePassed = duration;
        ResetTimeDisplay();
    }

    private void Update()
    {
        if (countingDown)
        {
            if (entered)
            {
                timePassed -= Time.deltaTime;
                if (timePassed > 0)
                {
                    timerLabel.text = string.Format("00:{0:D2}", (int)Mathf.Floor(timePassed));
                }
                else
                {
                    oCountDownCompleteEvent?.Invoke();
                    timerLabel.text = "00:00";
                    timePassed = duration;
                    countingDown = false;
                }
            }
            else
            {
                ResetTimeDisplay();
                timePassed = duration;
                countingDown = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!countingDown && other.CompareTag("Player"))
        {
            onTriggerEnterEvent?.Invoke();
            entered = true;
            countingDown = true;
        }
    }

    private void ResetTimeDisplay()
    {
        timerLabel.text = beforeCountDown;
    }

    public void StopCountDown()
    {
        entered = false;
        countingDown = false;
    }
}
