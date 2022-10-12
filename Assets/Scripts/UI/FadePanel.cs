/*  Filename:           FadePanel.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Used in UI when on enable; disable itself when finished fading
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadePanel : MonoBehaviour
{
    [SerializeField] private float waitingTime;
    [SerializeField] private float duration;
    [SerializeField] private Color initialColor;
    [SerializeField] private Color finalColor;

    private float timePassed;
    private Image fadeImage;

    private void OnEnable()
    {
        fadeImage = GetComponent<Image>();
        timePassed = 0;
        fadeImage.color = initialColor;
    }

    private void Update()
    {
        if (timePassed < waitingTime + duration)
        {
            timePassed += Time.deltaTime;

            if (timePassed > waitingTime)
            {
                fadeImage.color = Color.Lerp(initialColor, finalColor, (timePassed - waitingTime) / duration);
            }
        }
        else if (timePassed >= waitingTime + duration)
        {
            gameObject.SetActive(false);
        }
    }
}
