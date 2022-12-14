using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkSpiderState : BaseNetworkState<NetworkSpiderController>
{
    public NetworkSpiderState(NetworkSpiderController networkSpiderController) : base(networkSpiderController)
    {
        ;
    }

}
