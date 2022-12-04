/*  Filename:           SpiderPoolManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 13, 2022
 *  Description:        For creating spider object pooling
 *  Revision History:   November 13, 2022, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class SpiderPoolManager : PoolManager<SpiderController>
{
    public SpiderController GetPooledEnemy(Vector3 spawnPosition)
    {
        var newEnemy = GetPooledObject(spawnPosition, false);
        newEnemy.gameObject.SetActive(true);
        return newEnemy;
    }

    public void ReturnPooledEnemy(SpiderController returnedProjectile)
    {
        ReturnPooledObject(returnedProjectile.gameObject);
    }
}
