using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    // Game Object which is damaged by "this" object
    public string damagedTag;
    // Health System in damaged object
    public HealthSystem healthSystem;
    public float damage;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Every time it collides with the object in damages the object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(damagedTag))
        {
            healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            healthSystem.health -= damage;
            healthSystem.Damaged();
        }
    }
}

