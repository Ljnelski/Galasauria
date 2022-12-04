/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 16, 2022
 *  Description:        Controls Plasma Caster
 *  Revision History:   October 16, 2022 (Liam Nelski): Initial script. *    
 *                      December 3rd, 2022 (Liam Nelsik): Implemented
 */
using Assets.Scripts.InventorySystem.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCaster : EquipableItem
{
    [Header("Ammo Itemdata")]
    [SerializeField] private ItemData _plasmaCartridgeitemData;

    [Header("Stats")]
    [SerializeField] private float _plasmaboltSpeed;
    [SerializeField] private float _plasmaboltLifetime;

    [Header("Transforms")]
    [SerializeField] private Transform _plasmaboltSpawn;
    [SerializeField] private Transform _barrel;

    [Header("Scripts")]
    [SerializeField] private PlasmaboltPoolManager _plasmaboltPool;
    [SerializeField] private ActionTimerPool _timers;
    [SerializeField] private Animator _animator;

    public override void BeginUse(GameEnums.EquipableInput attack) 
    {
        Debug.Log("Begin Use");
        ItemOwnerInventory.itemDictionary.TryGetValue(_plasmaCartridgeitemData, out Item plasmaCartridge);
        // Check if there is ammo
        if (plasmaCartridge == null) 
        {
            return;
        }

        if(plasmaCartridge.stackSize < 1)
        {
            return;
        }

        Plasmabolt newPlasmabolt = _plasmaboltPool.GetPooledPlasmabolt(_plasmaboltSpawn.position);
        newPlasmabolt.Shoot(_plasmaboltSpeed);
        InUse = true;

        // Make Sure the plamabolt destroys itself
        _timers.CreateTimer(_plasmaboltLifetime, () => { newPlasmabolt.Disable(0); });

        // Remove ammunition from inventory
        ItemOwnerInventory.RemoveItem(_plasmaCartridgeitemData);

        // Animate Shoot 
        _animator.SetTrigger("shoot");
    }

    public override void EndUse()
    {
        Debug.Log("End use");
         InUse = false;
    }

    private void ExtendBarrel(float Distance)
    {
        _barrel.position = new Vector3(_barrel.position.x, _barrel.position.y, Distance);
    }    
}
