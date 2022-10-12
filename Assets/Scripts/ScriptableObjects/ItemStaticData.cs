/*  Filename:           ItemStaticData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Stores item data list and used in item manager
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStaticData", menuName = "ScriptableObjects/ItemStaticData", order = 1)]
public class ItemStaticData : ScriptableObject
{
    public List<ItemData> itemList;
}
