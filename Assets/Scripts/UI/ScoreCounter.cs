/*  Filename:           ScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 8, 2022
 *  Description:        Contains public function to increase the score displayed on the UI
 *  Revision History:   November 8, 2022 (Yuk Yee Wong): Initial script.
 *                      November 12, 2022 (Yuk Yee Wong): Assign the inventoryIncrementAction
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private List<AnimatedDigit> animatedDigits;
    [SerializeField] private float intialDigitDuration;
    [SerializeField] private int maxSkipDigit;
    [SerializeField] private float minSize;
    [SerializeField] private float normalSize;
    [SerializeField] private float maxSize;

    public int CurrentScore { get; private set; }
    private int maxScore;

    void Awake()
    {
        StartCoroutine(WaitForManagerInstance());
    }

    private IEnumerator WaitForManagerInstance()
    {
        if (InventoryManager.Instance == null || InventoryManager.Instance.Inventories.Count == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        InventoryManager.Instance.Inventories[0].inventoryIncrementAction += IncreaseItemScore;
    }

    private void IncreaseItemScore(ItemData itemData)
    {
        IncreaseScore(itemData.score);
    }

    void Start()
    {
        maxScore = (int)Mathf.Pow(10, animatedDigits.Count) - 1;

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

        // InvokeRepeating("Test", 1, 3);
    }

    /*
    private void Test()
    {
        IncreaseScore(1000);
    }*/

    private void PlaySound()
    {
        SoundManager.Instance.PlayGeneralAudio(GameEnums.GeneralAudio.SCORE);
    }

    public void ResetScore()
    {
        foreach (AnimatedDigit digit in animatedDigits)
        {
            digit.ResetDigit();
        }
        CurrentScore = 0;
    }

    public void IncreaseScore(int increment)
    {
        if (animatedDigits.Count == 0)
        { 
            return;
        }

        int newScore = CurrentScore + increment;

        if (newScore > maxScore)        
        {
            newScore = maxScore;

            Debug.LogWarning($"full increment {increment} cannot be displayed because it exceeds the digits provided (p.s. max {maxScore}");
        }

        if (newScore - CurrentScore > 0)
        {
            animatedDigits[0].Increase(newScore - CurrentScore);
            CurrentScore = newScore;
        }
        else if (newScore - CurrentScore < 0)
        {
            Debug.LogWarning("Decrement is not implemented");
        }
    }
}
