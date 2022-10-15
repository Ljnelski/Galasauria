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
            Debug.Log("Collector ERROR: Inventory does not exist on GameObject");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision");
        Collectable collectable = collision.GetComponent<Collectable>();
        if(collectable != null)
        {
            collectable.collectItem(inventory);
        }
    }
}
