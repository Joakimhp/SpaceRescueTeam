using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOverviewUIHandler : MonoBehaviour
{
    private GameObject npcUIPrefab;
    List<GameObject> npcUIObjects;

    public void Initialize( List<Transform> npcTransforms ) {
        npcUIPrefab = Resources.Load<GameObject> ( "NPCUIPrefab" );
        npcUIObjects = new List<GameObject> ();
        
        for ( int i = 0; i < npcTransforms.Count; i++ ) {
            GameObject obj = Instantiate ( npcUIPrefab , transform );
            npcUIObjects.Add ( obj );
        }

        SetScore ( 0 );
    }

    public void UpdateUI(int score) {
        SetScore ( score );
    }

    private void SetScore( int score ) {
        for ( int i = 0; i < npcUIObjects.Count; i++ ) {
            Color newColor;
            if ( i < score) {
                newColor = new Color ( 1f , 1f , 1f , 1f );
            } else {
                newColor = new Color ( 1f , 1f , 1f , 0.5f );
            }
            
            npcUIObjects [ i ].GetComponent<Image> ().color = newColor;
        }
    }


}
