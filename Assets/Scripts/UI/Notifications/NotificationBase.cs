/*  Filename:           NotificationBase.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Show and fade out notification
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class NotificationBase : MonoBehaviour
{
    [SerializeField] private float waitingTime;
    [SerializeField] private float duration;

    private float timePassed;
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        timePassed = 0;
        canvasGroup.alpha = 1;
    }

    private void Update()
    {
        if (timePassed < waitingTime + duration)
        {
            timePassed += Time.deltaTime;

            if (timePassed > waitingTime)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, (timePassed - waitingTime) / duration);
            }
        }
        else if (timePassed >= waitingTime + duration)
        {
            // TODO, recycle
            Destroy(gameObject);
        }
    }
}
