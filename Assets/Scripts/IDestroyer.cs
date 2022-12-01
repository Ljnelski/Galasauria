/*  Filename:           IDestroyer.cs
 *  Author:             Jinkyu Choi (301024988), Yuk Yee Wong (301234795)
 *  Last Update:        November 25, 2022
 *  Description:        Interface to use in object that does Damage
 *  Revision History:   November 13, 2022 (Jinkyu Choi): Initial Script.
 *                      November 13, 2022 (Jinkyu Choi): Write Changes here
 *                      November 25, 2022 (Yuk Yee Wong): Rename the script from IDestroyingObject to IDestroyer.
 */

using System;

public interface IDestroyer
{
    public float Damage { get; set; }

    public void OnTargetDestroyed(int score);
}
