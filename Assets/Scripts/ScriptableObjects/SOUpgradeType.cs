using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "UpgradeType" , menuName = "Personal/Upgrades" )]
public class SOUpgrades : ScriptableObject
{
    public List<Upgrade> upgrades;
}

[System.Serializable]
public class Upgrade
{
    public string name;
    public  List<UpgradeData> upgradeDatas;
}

[System.Serializable]
public class UpgradeData
{
    public Sprite sprite;
    public int upgradeCost;
}