/*  Filename:           InventoryItemDisplay.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Used in inventory screen to show inventory icon and count
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField] private string countFormat = "x {0}";
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemCountLabel;
    [SerializeField] private Text itemNameLabel; // temp

    public void Init(Sprite icon, int count, string name)
    {
        itemIcon.sprite = icon;
        itemCountLabel.text = string.Format(countFormat, count);
        itemNameLabel.text = name;
    }
}
