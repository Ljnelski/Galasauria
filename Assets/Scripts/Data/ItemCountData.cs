/*  Filename:           ItemCountData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Stores the item type and the count
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

[System.Serializable]
public class ItemCountData
{
    public GameEnums.ItemType type;
    public int count;
}
