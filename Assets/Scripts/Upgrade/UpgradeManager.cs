using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public event EventHandler OnUpgradeLevelUp;

    private UpgradeListSO upgradeListSO;

    private Dictionary<Upgrade, UpgradeSO> upgradeDictionary;
    private Dictionary<UpgradeSO, int> upgradeLevelDictionary;

    private int maxLevel = 20;

    private void Awake()
    {
        Instance = this;

        upgradeListSO = Resources.Load<UpgradeListSO>(typeof(UpgradeListSO).ToString());

        upgradeDictionary = new Dictionary<Upgrade, UpgradeSO>();

        foreach (UpgradeSO upgradeSO in upgradeListSO.UpgradeList)
        {
            upgradeDictionary[upgradeSO.upgrade] = upgradeSO;
        }

        upgradeLevelDictionary = new Dictionary<UpgradeSO, int>();

        foreach (UpgradeSO upgradeSO in upgradeListSO.UpgradeList)
        {
            upgradeLevelDictionary[upgradeSO] = 0;
        }
    }

    public void LevelUpUpgrade(Upgrade upgrade)
    {
        upgradeLevelDictionary[upgradeDictionary[upgrade]]++;

        OnUpgradeLevelUp?.Invoke(this, EventArgs.Empty);
    }

    public float GetUpgradeValue(Upgrade upgrade)
    {
        return upgradeDictionary[upgrade].GetValue(GetUpgradeLevel(upgrade));
    }

    public int GetUpgradeLevel(Upgrade upgrade)
    {
        return upgradeLevelDictionary[upgradeDictionary[upgrade]];
    }

    public bool CanLevelUpUpgrade(Upgrade upgrade)
    {
        return upgradeLevelDictionary[upgradeDictionary[upgrade]] < maxLevel;
    }
}
