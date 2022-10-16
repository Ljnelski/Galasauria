/*  Filename:           InventoryManager.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Attaches a inventory to the gameobject
 *  Revision History:   October 12, 2022 (Liam Nelski): Initial script. *                     
 */

using System;
public interface IEquipable<TUpgrade> where TUpgrade : Enum
{
    public Action Attack { get; }
    public abstract void Upgrade();
}
