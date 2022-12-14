using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariablesHolder : MonoBehaviour
{
    public static int EnabledRequestRescue;

    private void OnDestroy()
    {
        EnabledRequestRescue = 0;
    }
}
