/*  Filename:           ItemReceivedNotification.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Show and fade out item received notification
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

public class ItemReceivedNotification : NotificationBase
{ 
    [SerializeField] private string receviedTextFormat = "{0} x {1}";
    [SerializeField] private Text itemReceivedLabel;
    [SerializeField] private Image iconImage;

    public void Init(Sprite itemIcon, string itemName, int itemCount)
    {
        iconImage.sprite = itemIcon;
        itemReceivedLabel.text = string.Format(receviedTextFormat, itemName, itemCount);
    }
}
