/*  Filename:           CyberBlade.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        December 3rd, 2022
 *  Description:        Pool for Plasmabolts
 *  Revision History:   December 3rd, 2022 (Liam Nelski): Initial script.
 */
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.InventorySystem.Weapons
{
    public class PlasmaboltPoolManager : PoolManager<Plasmabolt>
    {
        public Plasmabolt GetPooledPlasmabolt(Vector3 spawnPosition)
        {
            Plasmabolt newPlasmabolt = GetPooledObject(spawnPosition, true);
            if(newPlasmabolt.plasmaboltPool == null)
            {
                newPlasmabolt.plasmaboltPool = this;
            }

            newPlasmabolt.gameObject.SetActive(true);
            return newPlasmabolt;
        }

        public void ReturnPooledPlasmabolt(Plasmabolt Plasmabolt)
        {
            ReturnPooledObject(Plasmabolt.gameObject);
        }
    }
}