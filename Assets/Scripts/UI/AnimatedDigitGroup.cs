/*  Filename:           AnimatedDigitGroup.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Manage a group of animated digits
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

public class AnimatedDigitGroup : MonoBehaviour
{
    [SerializeField] private List<AnimatedDigit> animatedDigits;
    [SerializeField] private float intialDigitDuration;
    [SerializeField] private int maxSkipDigit;
    [SerializeField] private float minSize;
    [SerializeField] private float normalSize;
    [SerializeField] private float maxSize;

    protected int CurrentNumber { get; private set; }
    private int maxNumber;
    private bool initied;

    void Start()
    {
        Initiate();

        // InvokeRepeating("Test", 1, 3);
    }

    /*
    private void Test()
    {
        IncreaseScore(1000);
    }*/

    private void Initiate()
    {
        if (!initied)
        {
            maxNumber = (int)Mathf.Pow(10, animatedDigits.Count) - 1;

            for (int i = 0; i < animatedDigits.Count; i++)
            {
                animatedDigits[i].Init(minSize, normalSize, maxSize, maxSkipDigit, intialDigitDuration);

                if (i + 1 < animatedDigits.Count)
                {
                    animatedDigits[i].AssignNextAnimatedDigit(animatedDigits[i + 1]);
                }

                if (i == 0)
                {
                    animatedDigits[i].ReachTargetNumber += PlaySound;
                }

                animatedDigits[i].ResetDigit();
            }

            initied = true;
        }
    }

    private void PlaySound()
    {
        SoundManager.Instance.PlayGeneralAudio(GameEnums.GeneralAudio.SCORE);
    }

    protected void ResetDigits()
    {
        foreach (AnimatedDigit digit in animatedDigits)
        {
            digit.ResetDigit();
        }
        CurrentNumber = 0;
    }

    protected void Increase(int increment)
    {
        Initiate();

        if (animatedDigits.Count == 0)
        {
            return;
        }

        int newNumber = CurrentNumber + increment;

        if (newNumber > maxNumber)
        {
            newNumber = maxNumber;

            Debug.LogWarning($"full increment {increment} cannot be displayed because it exceeds the digits provided (p.s. max {maxNumber}");
        }

        if (newNumber - CurrentNumber > 0)
        {
            animatedDigits[0].Increase(newNumber - CurrentNumber);
            CurrentNumber = newNumber;
        }
        else if (newNumber - CurrentNumber < 0)
        {
            Debug.LogWarning("Decrement is not implemented");
        }
    }
}
