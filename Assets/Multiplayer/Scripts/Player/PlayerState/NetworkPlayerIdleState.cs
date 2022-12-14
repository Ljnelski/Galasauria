using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkPlayerIdleState : NetworkPlayerState
{
    public NetworkPlayerIdleState(NetworkPlayerController networkPlayerController) : base(networkPlayerController)
    {

    }
    public override void OnStateEnter()
    {
        ;
    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        MovePlayer();

        if (context.EquipedItem.ItemEquiped && context.AttackInput != GameEnums.EquipableInput.NONE && !context.EquipedItem.InUse)
        {
            context.UpdatePlayerActiveStateServerRpc(NetworkPlayerController.eNetworkPlayerState.USE_ITEM);
        }

        if (context.DashInput && context.CanDash)
        {
            context.UpdatePlayerActiveStateServerRpc(NetworkPlayerController.eNetworkPlayerState.DASH_STATE);
        }
    }
}

