using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkFinalScoreCounter : NetworkAnimatedDigitGroup<NetworkPlayerController>
{
    public override void SetUp(NetworkPlayerController networkPlayerController)
    {
        base.SetUp(networkPlayerController);

        if (_targetScript == null)
        {
            Debug.LogError("NetworkFinalScoreCounter ERROR: HUDTargetPlayer Script does not have a valid Target");
            return;
        }
    }

    void OnEnable()
    {
        if (_targetScript != null)
        {
            DrawNumber(_targetScript.CurrentScore);
        }
    }
}