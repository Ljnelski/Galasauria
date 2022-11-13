/*  Filename:           NotificationController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Control and show notification in gameplay
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 12, 2022 (Yuk Yee Wong): Remove instance (singleton), assign inventoryIncrementAction
 *                      November 13th, 20022 (Liam Nelski) 
 */

using System.Collections;
using UnityEngine;

public class NotificationController : UIPlayerReader<Inventory>
{
    [SerializeField] private ItemReceivedNotification itemReceivedNotificationPrefab;
    [SerializeField] private Transform notificationContainer;

    private void Start()
    {
        GetTargetScript();

        _targetScript.inventoryIncrementAction += Notify;
    }

    private void Notify(ItemData itemData)
    {
        // TODO, recycle
        ItemReceivedNotification notification = Instantiate(itemReceivedNotificationPrefab, notificationContainer);
        notification.Init(itemData.icon, itemData.itemName, 1);
    }
    private void OnDestroy()
    {
        _targetScript.inventoryIncrementAction -= Notify;
    }
}
