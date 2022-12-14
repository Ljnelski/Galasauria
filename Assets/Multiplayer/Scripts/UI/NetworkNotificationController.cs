using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkNotificationController : UINetworkPlayerMonoBehaviourDataReader<Inventory>
{
    [SerializeField] private ItemReceivedNotification itemReceivedNotificationPrefab;
    [SerializeField] private Transform notificationContainer;

    public override void SetUp(Inventory inventory)
    {
        base.SetUp(inventory);

        if (_targetScript == null)
        {
            Debug.LogError("NetworkNotificationController ERROR: HUDTargetPlayer Script does not have a valid Target");
            return;
        }

        _targetScript.OnAddItem += Notify;
    }

    private void Notify(ItemData itemData)
    {
        // TODO, recycle
        ItemReceivedNotification notification = Instantiate(itemReceivedNotificationPrefab, notificationContainer);
        notification.Init(itemData.icon, itemData.itemName, 1);
    }

    private void OnDestroy()
    {
        if (_targetScript != null)
        {
            _targetScript.OnAddItem -= Notify;
        }
    }
}
