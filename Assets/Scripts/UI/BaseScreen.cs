/*  Filename:           BaseScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        Define time scale on enable and on disable
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 *                      November 25, 2022 (Yuk Yee Wong): Change OnEnable and OnDisable to virtual functions.
 */

using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        Time.timeScale = 0;
    }

    protected virtual void OnDisable()
    {
        Time.timeScale = 1;
    }
}
