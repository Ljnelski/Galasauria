using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDetailsDisplay : UIPlayerDataReader<PlayerController>
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemNameLabel;
    [SerializeField] private Text itemDescriptionLabel;
    [SerializeField] private Button useItemButton;

    void OnEnable()
    {
        GetTargetScript();
        ResetDisplay();
    }

    private void ResetDisplay()
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
        if (itemData.prefab.TryGetComponent(out UseableItem useableItem))
        {
            useableItem.InitEffect(_targetScript, itemData);
            useItemButton.gameObject.SetActive(true);            
            useItemButton.onClick.AddListener(useableItem.GetItemEffect());
            useItemButton.onClick.AddListener(() => { useItemButton.onClick.RemoveAllListeners(); });
        }       
    }        
}
