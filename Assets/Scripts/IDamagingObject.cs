/*  Filename:           IDamagingObject.cs
 *  Author:             Jinkyu Choi (301024988)
 *  Last Update:        November 13, 2022
 *  Description:        Interface to use in object that does Damage
 *  Revision History:   November, 13, 2022 (Jinkyu Choi): Initial Script.
 *                      November, 13, 2022 (Jinkyu Choi): Write Changes here
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagingObject
{
    public float Damage { get; set; }
}
