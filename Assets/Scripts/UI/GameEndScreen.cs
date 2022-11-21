/*  Filename:           GameEndScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Contains public open function to display win or lose
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class GameEndScreen : BaseScreen
{
    [SerializeField] private GameObject winLabel;
    [SerializeField] private GameObject loseLabel;

    public void Open(bool win)
    {
        gameObject.SetActive(true);
        winLabel.SetActive(win);
        loseLabel.SetActive(!win);
    }
}
