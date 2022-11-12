/*  Filename:           DamageSystem.cs
 *  Author:             Jinkyu Choi (301024988)
 *  Last Update:        November 11, 2022
 *  Description:        Script which manages the damage toward opponent
 *  Revision History:   November, 11, 2022 (Jinkyu Choi): Initial Script.
 *                      November, 11, 2022 (Jinkyu Choi): Write Changes here
 */

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

