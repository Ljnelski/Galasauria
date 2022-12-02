/*  Filename:           AnimatedDigitGroup.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Manage a group of animated digits
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 *                      November 13, 2022 (Liam Nelski): Renamed Increment => DrawNumber and made it so the new number is the draw number, not added onto the existing value
 */

using System.Collections.Generic;
using UnityEngine;

public class AnimatedDigitGroup<TTargetScript> : UIPlayerDataReader<TTargetScript> where TTargetScript : MonoBehaviour
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

    protected virtual void Start()
    {
        GetTargetScript();
        Initiate();
    }

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

    protected void DrawNumber(int newNumber)
    {
        Initiate();

        if (animatedDigits.Count == 0)
        {
            return;
        }

        if (newNumber > maxNumber)
        {
            newNumber = maxNumber;

            Debug.LogWarning($"full increment {newNumber} cannot be displayed because it exceeds the digits provided (p.s. max {maxNumber}");
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
