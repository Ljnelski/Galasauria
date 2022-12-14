using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerUseItemState : NetworkPlayerState
{
    public NetworkPlayerUseItemState(NetworkPlayerController networkPlayerController) : base(networkPlayerController)
    {
    }

    public override void OnStateEnter()
    {
        context.EquipedItem.UseItem(context.AttackInput);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateRun()
    {
        MovePlayer();

        if (!context.EquipedItem.InUse)
        {
            context.UpdatePlayerActiveStateServerRpc(NetworkPlayerController.eNetworkPlayerState.IDLE);
        }
    }
}
