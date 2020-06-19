using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class UIHealthOverviewUIHandler : MonoBehaviour
{
    private GameObject UIHealthPrefab;
    private List<GameObject> UIhealthObjects;

    PlayerController player;

    public void Initialize( PlayerController player ) {
        UIhealthObjects = new List<GameObject> ();

        this.player = player;

        UIHealthPrefab = Resources.Load<GameObject> ( "UIHealthPrefab" );
        for ( int i = 0; i < player.health; i++ ) {
            GameObject obj = Instantiate ( UIHealthPrefab , transform );
            UIhealthObjects.Add ( obj );
        }
    }

    public void UpdateUI() {
        for ( int i = 0; i < UIhealthObjects.Count; i++ ) {
            if ( i < player.health ) {
                UIhealthObjects [ i ].gameObject.SetActive ( true );
            } else {
                UIhealthObjects [ i ].gameObject.SetActive ( false );
            }
        }
    }
}
