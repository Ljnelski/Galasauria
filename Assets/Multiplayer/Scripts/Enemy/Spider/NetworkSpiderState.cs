/*  Filename:           NetworkSpiderState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

public abstract class NetworkSpiderState : BaseNetworkState<NetworkSpiderController>
{
    public NetworkSpiderState(NetworkSpiderController networkSpiderController) : base(networkSpiderController)
    {
        ;
    }

}
