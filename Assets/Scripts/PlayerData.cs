using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int [] upgradeLevels; //health, gun level, bulletSpeed
    public int gold = 0;

    public PlayerData(int upgradesCount) {
        upgradeLevels = new int [ upgradesCount ];
        for ( int i = 0; i < upgradeLevels.Length; i++ ) {
            upgradeLevels [ i ] = 0;
        }
    }

    public bool CanBuyForGold(int price) {
        if(gold > price ) {
            return true;
        }
        return false;
    }

    public void SubtractGold(int price) {
        gold -= price;
        SaveSystem.SavePlayer ( this );
    }
}
