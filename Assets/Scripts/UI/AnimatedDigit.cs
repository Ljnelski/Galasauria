/*  Filename:           AnimatedDigit.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 8, 2022
 *  Description:        Used by score counter to animate score increment by resizing the text
 *  Revision History:   November 8, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AnimatedDigit : MonoBehaviour
{
    private float minSize;
    private float normalSize;
    private float maxSize;
    private int maxSkipDigit;
    private float duration;
    private AnimatedDigit nextDigit;
    private TextMeshProUGUI label;
    private int currentNumber;
    private float timePassed;
    private bool animated;
    private int pendingIncrement;

    public Action ReachTargetNumber;

    void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (animated)
        {
            if (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                label.fontSize = Mathf.Lerp(minSize, maxSize, timePassed / duration);
            }
            else
            {
                timePassed = 0;
                animated = false;
                label.fontSize = normalSize;

                if (pendingIncrement > 0)
                {
                    AnimateIncrement();
                }
                else
                {
                    ReachTargetNumber?.Invoke();
                }
            }
        }
    }

    public void ResetDigit()
    {
        animated = false;
        timePassed = 0;
        currentNumber = 0;
        pendingIncrement = 0;

        if (label == null)
        {
            label = GetComponent<TextMeshProUGUI>();
        }

        label.text = "0";
        label.fontSize = normalSize;
    }

    public void Init(float minSize, float normalSize, float maxSize, int maxSkipDigit, float duration)
    {
        this.minSize = minSize;
        this.normalSize = normalSize;
        this.maxSize = maxSize;
        this.maxSkipDigit = maxSkipDigit;
        this.duration = duration;
    }

    public void AssignNextAnimatedDigit(AnimatedDigit nextDigit)
    {
        this.nextDigit = nextDigit;
    }

    /// <summary>
    /// Number should between 1 to 10.
    /// </summary>
    /// <param name="targetNumber"></param>
    public void Increase(int increment)
    {
        pendingIncrement += increment;
        AnimateIncrement();
    }

    private void AnimateIncrement()
    {
        int i = maxSkipDigit;
        bool jumped = false;
        do
        {
            int unit = (int)Mathf.Pow(10, i);

            if (pendingIncrement >= unit && unit >= 10)
            {
                pendingIncrement -= unit;
                nextDigit.Increase(unit / 10);
                jumped = true;
                break;
            }

            i--;
        }
        while (i > 0);        
        
        if (!jumped)
        {
            currentNumber++;
            pendingIncrement--;

            if (currentNumber == 10)
            {
                currentNumber = 0;
                nextDigit.Increase(1);
            }
        }

        label.text = currentNumber.ToString();
        timePassed = 0;
        animated = true;
    }
}
