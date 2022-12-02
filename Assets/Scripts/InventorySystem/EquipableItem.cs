/*  Filename:           IEquipable.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Abstract Class for Equipable Items
 *  Revision History:   October 12, 2022 (Liam Nelski): Initial script. *   
 *                      Novemeber 25th, 2022 (Liam Nelski): Modified to be a abstract class rather than interface renamed to EquipableItem from IEquipable
 */

using UnityEngine;

public abstract class EquipableItem : MonoBehaviour
{
    [field:SerializeField] public Transform LeftHandIKTransform { get; protected set; }
    [field:SerializeField] public Transform RightHandIKTransform { get; protected set; }

    public bool InUse { get; protected set; }
    public abstract void BeginUse(GameEnums.EquipableInput attack);
    public abstract void EndUse();

    
}