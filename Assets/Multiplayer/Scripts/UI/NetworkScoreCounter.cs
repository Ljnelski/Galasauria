using System.Collections;
using System.Collections.Generic;
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