/*  Filename:           PickupItemTrigger.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        Player can pick up item and add to inventory when collide with the trigger
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItemTrigger : MonoBehaviour
{
    [SerializeField] private List<ItemCountData> itemCountData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (ItemCountData data in itemCountData)
            {
                InventoryManager.Instance.AddItem(data.type, data.count);
            }

            // TODO, recycle
            Destroy(gameObject);
        }
    }
}
