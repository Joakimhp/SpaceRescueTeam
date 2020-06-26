using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    Transform [] spawnPoints;
    GameObject meteorPrefab;

    public void Initialize() {
        spawnPoints = transform.GetComponentsInChildrenWithoutParent<Transform> ();
        meteorPrefab = Resources.Load<GameObject> ( "MeteorPrefab" );
        StartCoroutine ( SpawnMeteorTimer () );
    }

    private void SpawnMeteor() {
        int randomIndex = Random.Range ( 0 , spawnPoints.Length );
        Meteor newMeteor = Instantiate ( meteorPrefab , spawnPoints [ randomIndex ].position , Quaternion.identity ).GetComponent<Meteor> ();

        Vector2 direction = new Vector2 ();
        direction.x = spawnPoints [ randomIndex ].position.x > 0 ? -1 : 1;
        int size = Random.Range ( 3 , 5 );

        newMeteor.Initialize ( size , size , direction , meteorPrefab );
    }

    private IEnumerator SpawnMeteorTimer() {
        while ( true ) {
            SpawnMeteor ();

            yield return new WaitForSeconds ( 7f );
        }
    }
}
