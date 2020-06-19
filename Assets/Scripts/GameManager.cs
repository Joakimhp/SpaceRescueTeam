using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    UIInGameHandler uiInGameHandler;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<PlayerController> ();
        player.Initialize ( this , SaveSystem.LoadPlayer () );

        uiInGameHandler = GetComponentInChildren<UIInGameHandler> ();
        uiInGameHandler.Initialize (player);

        GameObject meteorPrefab = Resources.Load<GameObject> ( "MeteorPrefab" );
        Meteor meteor = Instantiate ( meteorPrefab , new Vector3 ( 3.39f , 1.63f , 0 ) , Quaternion.identity ).GetComponent<Meteor> ();
        meteor.Initialize ( 4 , 4 , Vector2.zero , meteorPrefab );
    }

    public void GameOver(bool playerWon) {
        UpdateUI ();
        uiInGameHandler.ShowGameOverScreen (playerWon);
    }

    public void UpdateUI() {
        uiInGameHandler.UpdateUI ();
    }

}
