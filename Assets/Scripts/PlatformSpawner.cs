using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private GameObject platformPrefab;

    private GameObject platform;
    private GameObject finishAreaGameObject;

    private SpriteRenderer groundSprite;

    Vector2 minPosition, maxPosition;

    public void Initialize( List<Transform> npcTransforms , SpriteRenderer groundSprite ) {
        platformPrefab = Resources.Load<GameObject> ( "PlatformPrefab" );
        this.groundSprite = groundSprite;

        minPosition = Camera.main.ViewportToWorldPoint ( new Vector3 ( 0 , 0 , 0 ) );
        minPosition.x += platformPrefab.GetComponent<SpriteRenderer> ().sprite.bounds.extents.x;
        maxPosition = Camera.main.ViewportToWorldPoint ( new Vector3 ( 1 , 1 , 0 ) );
        maxPosition.x -= platformPrefab.GetComponent<SpriteRenderer> ().sprite.bounds.extents.x;

        Spawn ( npcTransforms );

        Transform [] tmpGameObjects = platform.GetComponentsInChildren<Transform> ();
        finishAreaGameObject = tmpGameObjects [ tmpGameObjects.Length - 1 ].gameObject;
        finishAreaGameObject.SetActive ( false );
    }

    public void Spawn( List<Transform> npcTransforms ) {
        platform = Instantiate ( platformPrefab , Vector3.zero , Quaternion.identity );
        SpriteRenderer platformSpriteRenderer = platform.GetComponent<SpriteRenderer> ();
        Move ( npcTransforms , platformSpriteRenderer );
    }

    public void Move( List<Transform> npcTransforms , SpriteRenderer platformSpriteRenderer ) {
        
        float randomXCoord = Random.Range ( minPosition.x , maxPosition.x );
        float yCoord = groundSprite.transform.position.y + ( groundSprite.sprite.bounds.max.y * groundSprite.transform.localScale.y );

        platform.transform.position = new Vector3 ( randomXCoord , yCoord , 0 );

        if ( CheckForOverlap ( npcTransforms , platformSpriteRenderer ) ) {
            Move ( npcTransforms , platformSpriteRenderer );
        }
    }

    public bool CheckForOverlap( List<Transform> transformsToCompareWith , SpriteRenderer platformSpriteRenderer ) {
        foreach ( Transform currentTransform in transformsToCompareWith ) {
            SpriteRenderer currentTransformSpriteRenderer = currentTransform.GetComponent<SpriteRenderer> ();
            if ( currentTransformSpriteRenderer == null ) {
                continue;
            }
            float distanceToTransform = Vector2.Distance ( platform.transform.position , currentTransform.position );
            float platformSpriteExtent = platformSpriteRenderer.sprite.bounds.extents.x * platformSpriteRenderer.transform.localScale.x;
            float currentTransformSpriteExtent = currentTransformSpriteRenderer.sprite.bounds.extents.x * currentTransformSpriteRenderer.transform.localScale.x;
            float distanceThreshold = platformSpriteExtent + currentTransformSpriteExtent;
            if ( distanceToTransform < distanceThreshold ) {
                return true;
            }
        }
        return false;
    }

    public void ActivateEndBeam() {
        finishAreaGameObject.SetActive ( true );
    }

    public Vector3 GetPlayerSpawnPoint() {
        Transform[] tmpTransforms = platform.GetComponentsInChildren<Transform> ();
        Transform playerSpawnPoint = tmpTransforms [ tmpTransforms.Length - 1 ];
        Debug.Log(playerSpawnPoint.gameObject.name);
        return playerSpawnPoint.position;
    }

    //private void OnDrawGizmos() {
    //    if ( platform != null ) {
    //        SpriteRenderer sr = platform.GetComponent<SpriteRenderer> ();
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine ( sr.sprite.pivot , sr.sprite.pivot + sr.sprite.bounds.size.GetXYVector2 () );
    //    }
    //}
}
