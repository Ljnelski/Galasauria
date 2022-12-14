/*  Filename:           NetworkPlayerIdleState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

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

