using UnityEngine;

public class CyberBladeUpgradeButton :UIPlayerDataReader<PlayerController>
{
    [SerializeField] private CyberBladeUpgrade cyberBladeUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        GetTargetScript();
    }

    public void Upgrade()
    {
        _targetScript.Upgrade(cyberBladeUpgrade);
    }
}