/*  Filename:           NotificationController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Control and show notification in gameplay
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public static NotificationController Instance;

    [SerializeField] private ItemReceivedNotification itemReceivedNotificationPrefab;
    [SerializeField] private Transform notificationContainer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Notify(Sprite itemIcon, string itemName, int itemCount)
    {
        Debug.Log(itemName + " " + itemCount);
        // TODO, recycle
        ItemReceivedNotification notification = Instantiate(itemReceivedNotificationPrefab, notificationContainer);
        notification.Init(itemIcon, itemName, itemCount);
    }
}
