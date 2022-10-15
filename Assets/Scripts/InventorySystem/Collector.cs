/*  Filename:           Collector.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Finds inventory attached 
 *  Revision History:   October 14, 2022 (Liam Nelski): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    Inventory inventory;

    private void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
        if(inventory == null)
        {
            Debug.LogError("Collector ERROR: Inventory does not exist on GameObject");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {        
        if(collision.CompareTag("Collectable"))
        {            
            ICollectable collectable = collision.GetComponent<ICollectable>();
            if (collectable != null)
            {
                collectable.Collect(inventory);
            }
        }        
    }
}
