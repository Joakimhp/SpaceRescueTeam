using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<PlayerController> ();
        player.SetData ( SaveSystem.LoadPlayer () );
        
    }
    
}
