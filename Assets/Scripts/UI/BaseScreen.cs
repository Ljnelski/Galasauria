/*  Filename:           BaseScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        Define time scale on enable and on disable
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    protected void OnEnable()
    {
        Time.timeScale = 0;
    }

    protected void OnDisable()
    {
        Time.timeScale = 1;
    }
}
