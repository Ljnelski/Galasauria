using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCasterUpgradeButton : UIPlayerDataReader<PlayerController>
{
    [SerializeField] private PlasmaCasterUpgrade plasmaCasterUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        GetTargetScript();
    }

    public void Upgrade()
    {
        _targetScript.Upgrade(plasmaCasterUpgrade);
    }
}
