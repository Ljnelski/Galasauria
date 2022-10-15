/*  Filename:           ItemData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *                      Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Stores the item data (i.e. type, name, description) and the icon sprite
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Items/ItemData", fileName = "ItemData")]
public class ItemData : ScriptableObject
{    
    public string id;
    public string name;
    public string description;
    public GameEnums.ItemType type;
    [Header("Stats")]
    public int maxStack;
    public int itemWeight;
    [Header("Visual")]
    public GameObject prefab;
    public Sprite icon;

}
