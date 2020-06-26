using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class NPCSpawner : MonoBehaviour
{
    Vector2 minPosition, maxPosition;

    GameObject npcPrefab;

    List<Transform> npcTransforms;

    public void Initialize( int npcToSpawn , SpriteRenderer groundSprite ) {
        npcPrefab = Resources.Load<GameObject> ( "Human NPC" );
        npcTransforms = new List<Transform> ();

        minPosition = Camera.main.ViewportToWorldPoint ( new Vector3 ( 0 , 0 , 0 ) );
        minPosition.x += npcPrefab.GetComponent<SpriteRenderer> ().sprite.bounds.extents.x;
        maxPosition = Camera.main.ViewportToWorldPoint ( new Vector3 ( 1 , 1 , 0 ) );
        maxPosition.x -= npcPrefab.GetComponent<SpriteRenderer> ().sprite.bounds.extents.x;

        Spawn ( npcToSpawn , groundSprite );
        //Vector3 npcPosition = new Vector3 ( Random.Range ( minPosition.x , maxPosition.x ) , yCoord , 0 );

    }

    private void Spawn( int npcToSpawn , SpriteRenderer groundSprite ) {
        for ( int i = 0; i < npcToSpawn; i++ ) {
            GameObject npc = Instantiate ( npcPrefab , Vector3.zero , Quaternion.identity );
            npcTransforms.Add ( npc.transform );
        }

        MoveAllNPC ( groundSprite );
    }

    private void MoveAllNPC( SpriteRenderer groundSprite ) {
        float yCoord = groundSprite.transform.position.y + ( groundSprite.sprite.bounds.max.y * groundSprite.transform.localScale.y );

        for ( int i = 0; i < npcTransforms.Count; i++ ) {
            MoveSpecificNPC ( i , yCoord );
            SpriteRenderer currentSpriteRenderer = npcTransforms [ i ].GetComponent<SpriteRenderer> ();
            while ( CheckForOverlapping ( i , currentSpriteRenderer) ) {
                MoveSpecificNPC ( i , yCoord );
                Debug.Log ( "Moving NPC" );
            }
        }
    }

    private void MoveSpecificNPC( int npcIndex , float yCoord ) {
        float randomXCoord = Random.Range ( minPosition.x , maxPosition.x );
        npcTransforms [ npcIndex ].transform.position = new Vector3 ( randomXCoord , yCoord , 0 );
    }

    private bool CheckForOverlapping( int npcIndex , SpriteRenderer currentSpriteRenderer) {
        //foreach ( Transform currentTransform in transformsToCompareWith ) {
        //    SpriteRenderer currentTransformSpriteRenderer = currentTransform.GetComponent<SpriteRenderer> ();
        //    if ( currentTransformSpriteRenderer == null ) {
        //        continue;
        //    }
        //    float distanceToTransform = Vector2.Distance ( platform.transform.position , currentTransform.position );
        //    float distanceThreshold = platformSpriteRenderer.sprite.bounds.extents.x + currentTransformSpriteRenderer.sprite.bounds.extents.x;
        //    if ( distanceToTransform < distanceThreshold ) {
        //        return true;
        //    }
        //}
        //return false;
        
        foreach ( Transform otherTransform in npcTransforms ) {
            if (otherTransform == npcTransforms [ npcIndex ] ) {
                continue;
            }

            SpriteRenderer otherSpriteRenderer = otherTransform.GetComponent<SpriteRenderer> ();
            if (otherSpriteRenderer == null ) {
                continue;
            }
            float distanceToTransform = Vector2.Distance ( npcTransforms [ npcIndex ].position , otherTransform.position );
            float currentSpriteExtent = currentSpriteRenderer.sprite.bounds.extents.x * currentSpriteRenderer.transform.localScale.x;
            float otherSpriteExtent = otherSpriteRenderer.sprite.bounds.extents.x * otherSpriteRenderer.transform.localScale.x;
            float distanceThreshold = currentSpriteExtent + otherSpriteExtent;
            if(distanceToTransform < distanceThreshold ) {
                return true;
            }

        }
            return false;
    }

    public List<Transform> GetNPCTransforms() {
        return npcTransforms;
    }

    public int GetAmountOfNPCs() {
        return npcTransforms.Count;
    }
}
