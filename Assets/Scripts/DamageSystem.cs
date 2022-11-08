using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    // Game Object which is damaged by "this" object
    public GameObject damagedObject;

    // Health System in damaged object
    public HealthSystem healthSystem;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        // Get Health System script from the object
        healthSystem = damagedObject.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Every time it collides with the object in damages the object
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == damagedObject)
        {
            healthSystem.health -= damage;
        }
    }
}

