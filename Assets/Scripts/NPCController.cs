using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private GameManager gameManager;

    private int baseGoldOnCollection = 10;
    private int goldOnCollection;

    public void Initialize( GameManager gameManager , int goldCollectionModifier ) {
        this.gameManager = gameManager;
        this.goldOnCollection = baseGoldOnCollection + ( 1 * goldCollectionModifier );

        spriteRenderer = GetComponent<SpriteRenderer> ();

        spriteRenderer.color = new Color ( Random.Range ( .3f , .8f ) , Random.Range ( .3f , .8f ) , Random.Range ( .3f , .8f ) );
    }

    public void ChildToTransform( Transform newParent ) {
        SpriteRenderer sr = GetComponent<SpriteRenderer> ();
        transform.position = newParent.transform.position - sr.transform.up * sr.sprite.bounds.size.y;
        transform.parent = newParent;
    }

    public void AddPointToScore() {
        gameManager.AddPointsAndGold ( 1 , goldOnCollection );
        Destroy ( gameObject );
    }
}
