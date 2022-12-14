using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkInventoryItemDetailsDisplay : UINetworkPlayerNetworkBehaviourDataReader<NetworkPlayerController>
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemNameLabel;
    [SerializeField] private Text itemDescriptionLabel;
    [SerializeField] private Button useItemButton;
    public Action RefreshInventoryScreen;

    void OnEnable()
    {
        ResetDisplay();
    }

    public override void SetUp(NetworkPlayerController networkPlayerController)
    {
        base.SetUp(networkPlayerController);

        if (_targetScript == null)
        {
            Debug.LogError("NetworkInventoryItemDetailsDisplay ERROR: HUDTargetPlayer Script does not have a valid Target");
            return;
        }
    }

    public void ResetDisplay()
    {
        itemIcon.gameObject.SetActive(false);
        itemNameLabel.text = "";
        itemDescriptionLabel.text = "";
        useItemButton.gameObject.SetActive(false);
    }

    public void Display(ItemData itemData)
    {
        itemIcon.sprite = itemData.icon;
        itemNameLabel.text = itemData.itemName;
        itemDescriptionLabel.text = itemData.description;

        itemIcon.gameObject.SetActive(itemIcon.sprite != null);

        // If the item is usable then add item effect to button listener
        if (itemData.prefab.TryGetComponent(out NetworkUseableItem networkUseableItem))
        {
            networkUseableItem.InitEffect(_targetScript, itemData);
            useItemButton.gameObject.SetActive(true);
            useItemButton.onClick.AddListener(networkUseableItem.GetItemEffect());
            useItemButton.onClick.AddListener(() => {
                useItemButton.onClick.RemoveAllListeners();
                RefreshInventoryScreen?.Invoke();
            });
        }
    }
}
