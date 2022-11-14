using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * THIS IS SAMPLE SCRIPT TO TEST THE DAMAGING METHOD DELETE IT AFTER IMPLEMENTATION
 * THIS IS SAMPLE SCRIPT TO TEST THE DAMAGING METHOD DELETE IT AFTER IMPLEMENTATION
 * THIS IS SAMPLE SCRIPT TO TEST THE DAMAGING METHOD DELETE IT AFTER IMPLEMENTATION
 * THIS IS SAMPLE SCRIPT TO TEST THE DAMAGING METHOD DELETE IT AFTER IMPLEMENTATION
 */
public class SampleScript4Damaging : MonoBehaviour,IDamagingObject
{
    public float Damage { get; set; }
    public float damage;
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        Damage = damage;
        HealthSystem healthSystem = this.gameObject.GetComponent<HealthSystem>();
        healthSystem.damaged += OnDamaged;
    } 

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnDamaged(float damage)
    {
        Debug.Log("ON DAMAGE CALLED");
        health -= damage;
    }
}
