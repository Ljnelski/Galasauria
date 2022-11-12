/*  Filename:           NotificationController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Control and show notification in gameplay
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 12, 2022 (Yuk Yee Wong): Remove instance (singleton), assign inventoryIncrementAction
 */

using System.Collections;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private ItemReceivedNotification itemReceivedNotificationPrefab;
    [SerializeField] private Transform notificationContainer;

    private void Awake()
    {
        StartCoroutine(WaitForManagerInstance());
    }

    private IEnumerator WaitForManagerInstance()
    {
        if (InventoryManager.Instance == null || InventoryManager.Instance.Inventories.Count == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        InventoryManager.Instance.Inventories[0].inventoryIncrementAction += Notify;
    }

    private void Notify(ItemData itemData)
    {
        // TODO, recycle
        ItemReceivedNotification notification = Instantiate(itemReceivedNotificationPrefab, notificationContainer);
        notification.Init(itemData.icon, itemData.itemName, 1);
    }
}
