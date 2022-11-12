/*  Filename:           HealthSystem.cs
 *  Author:             Jinkyu Choi (301024988)
 *  Last Update:        November 11, 2022
 *  Description:        Script which manages the health of the gameObject
 *  Revision History:   November, 11, 2022 (Jinkyu Choi): Initial Script.
 *                      November, 11, 2022 (Jinkyu Choi): Write Changes here
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public Slider? healthSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damaged()
    {
        if (healthSlider != null)
        {
            healthSlider.value = health / 100;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
