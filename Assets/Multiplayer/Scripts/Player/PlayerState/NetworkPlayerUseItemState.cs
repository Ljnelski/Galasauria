/*  Filename:           NetworkPlayerUseItemState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

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
