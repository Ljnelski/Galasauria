/*  Filename:           NetworkScoreCounter.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class NetworkScoreCounter : NetworkAnimatedDigitGroup<NetworkPlayerController>
{
    public override void SetUp(NetworkPlayerController networkPlayerController)
    {
        base.SetUp(networkPlayerController);

        if (_targetScript == null)
        {
            Debug.LogError("NetworkScoreCounter ERROR: HUDTargetPlayer Script does not have a valid Target");
            return;
        }

        _targetScript.OnScoreIncremented += IncreaseItemScore;
    }

    private void IncreaseItemScore(int increase)
    {
        DrawNumber(increase);
    }
}