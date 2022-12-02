/*  Filename:           InventoryItemDisplay.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Used in inventory screen to show inventory icon and count
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField] private string countFormat = "x {0}";
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemCountLabel;
    [SerializeField] private Text itemNameLabel; // temp
    [SerializeField] private Button button;

    private ItemData itemData;
    public Action<ItemData> OnItemClick;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void Init(ItemData itemData, int itemStackSize)
    {
        this.itemData = itemData;
        itemIcon.sprite = itemData.icon;
        itemCountLabel.text = string.Format(countFormat, itemStackSize);
        itemNameLabel.text = itemData.itemName;
    }

    private void OnButtonClick()
    {
        OnItemClick?.Invoke(itemData);
    }
}
