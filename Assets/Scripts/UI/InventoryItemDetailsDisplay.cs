using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDetailsDisplay : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemNameLabel;
    [SerializeField] private Text itemDescriptionLabel;
    [SerializeField] private Button useItemButton;

    void OnEnable()
    {
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

        useItemButton.gameObject.SetActive(
            itemData.type.Equals(GameEnums.ItemType.HEALTHPOWERUP)
            || itemData.type.Equals(GameEnums.ItemType.SPEEDPOWERUP));

        // TODO, wire up the power up function
    }
}
