/*  Filename:           IEquipable.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Interface for Equipable Items
 *  Revision History:   October 12, 2022 (Liam Nelski): Initial script. *                     
 */

public interface IEquipable
{
    public abstract bool InUse { get; }
    public abstract void BeginUse(GameEnums.EquipableInput attack);
    public abstract void EndUse();
}