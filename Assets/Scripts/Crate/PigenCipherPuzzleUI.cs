/*  Filename:           PigenCipherPuzzleUI.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 12, 2022
 *  Description:        UI script for pigen cipher puzzle
 *  Revision History:   November 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigenCipherPuzzleUI : MonoBehaviour
{
    [Header("Pigen Cipher")]
    [SerializeField] private List<Text> pigenCipherLabels;
    [SerializeField] private Text password;
    [SerializeField] private List<Text> answerLabels;
    [SerializeField] private int charCount;
    private string answerFormat = "{0}. {1}";

    public int AnswerIndex { get; private set; }
    public int AnswerLabelsCount { get { return answerLabels.Count; } }

    public void Refresh()
    {
        Tuple<string, int, List<string>, List<string>> pair = PigenCipherEncoder.GetPairs(charCount, answerLabels.Count);
        if (pair != null)
        {
            password.text = pair.Item1;
            AnswerIndex = pair.Item2;

            if (pigenCipherLabels.Count == pair.Item3.Count)
            {
                for (int i = 0; i < pigenCipherLabels.Count; i++)
                {
                    pigenCipherLabels[i].text = pair.Item3[i];
                }
            }
            else
            {
                Debug.LogWarning($"Number of pigpen cipher labels {pigenCipherLabels.Count} does not match with {pair.Item3.Count}");
            }

            if (answerLabels.Count == pair.Item4.Count)
            {
                for (int i = 0; i < answerLabels.Count; i++)
                {
                    answerLabels[i].text = string.Format(answerFormat, i + 1, pair.Item4[i]);
                }
            }
            else
            {
                Debug.LogWarning($"Number of answer labels {answerLabels.Count} does not match with {pair.Item4.Count} ");
            }
        }
    }
}
