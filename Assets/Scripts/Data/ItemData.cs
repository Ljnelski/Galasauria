/*  Filename:           ItemData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Stores the item data (i.e. type, name, description) and the icon sprite
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

[System.Serializable]
public class ItemData
{
    public GameEnums.ItemType type;
    public Sprite icon;
    public string name;
    public string description;
}
