/*  Filename:           GameEndTrigger.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        A trigger for game end state
 *  Revision History:   November 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameEndTrigger : MonoBehaviour
{
    [SerializeField] private GameEndScreen screen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            screen.Open(true);
        }
    }
}
