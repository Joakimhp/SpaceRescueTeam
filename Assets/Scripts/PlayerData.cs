using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class PlayerDataHandler
//{
//    public PlayerData playerData;

//    public PlayerDataHandler ( PlayerData player ) {
//        playerData.health = player.health;
//        playerData.gunLevel = player.gunLevel;
//        playerData.bulletSpeed = player.bulletSpeed;
//        playerData.gold = player.gold;
//    }
//}

[System.Serializable]
public class PlayerData
{
    public int health = 0;
    public int gunLevel = 0;
    public int bulletSpeed = 0;
    public int gold = 0;
}
