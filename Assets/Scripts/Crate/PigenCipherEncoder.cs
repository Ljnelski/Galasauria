/*  Filename:           PigenCipherEncoder.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 12, 2022
 *  Description:        Pigen cipher encoder to use in pigen cipher puzzle UI
 *  Revision History:   November 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System;
using System.Collections.Generic;

public class PigenCipherEncoder
{
    private static List<string> alphabets = new List<string> 
    { 
        "A", "B", "C", "D", "E"
        , "F", "G", "H", "i", "J"
        , "K", "L", "M", "N", "O"
        , "P", "Q", "R", "S", "T"
        , "U", "V", "W", "X", "Y"
        , "Z"
    };

    private static List<string> GetRandomCipherAlphabetList()
    {
        // Clone a list
        List<string> charList = new List<string>();
        charList.AddRange(alphabets);

        // Shuffle the list
        Shuffle(charList);

        return charList;
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, list.Count - 1);
            T temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }

    public static Tuple<string, int, List<string>, List<string>> GetPairs(int charCount, int answerCount)
    {
        if (charCount <= 0 || answerCount < 1)
        {
            return null;
        }

        string encoded = "";
        string decoded = "";
        List<string> alphabetList = GetRandomCipherAlphabetList();
        List<string> answerList = new List<string>();

        // correct answer
        for (int i = 0; i < charCount; i++)
        {
            int alphabetIndex = UnityEngine.Random.Range(0, alphabets.Count - 1);
            encoded += alphabets[alphabetIndex];
            decoded += alphabetList[alphabetIndex];
        }

        answerList.Add(decoded);

        // incorrect answers
        if (answerCount > 1)
        {
            while (answerList.Count < answerCount)
            {
                string incorrectAnswer = "";

                for (int i = 0; i < charCount; i++)
                {
                    int alphabetIndex = UnityEngine.Random.Range(0, alphabets.Count - 1);
                    incorrectAnswer += alphabets[alphabetIndex];
                }

                if (!answerList.Contains(incorrectAnswer))
                {
                    answerList.Add(incorrectAnswer);
                }
            }
        }

        // Shuffle the list
        Shuffle(answerList);

        return new Tuple<string, int, List<string>, List<string>>(encoded, answerList.IndexOf(decoded), alphabetList, answerList);
    }
}
