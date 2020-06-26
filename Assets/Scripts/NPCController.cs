using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    GameManager gameManager;

    public void Initialize(GameManager gameManager) {
        this.gameManager = gameManager;

        spriteRenderer = GetComponent<SpriteRenderer> ();

        spriteRenderer.color = new Color ( Random.Range ( .3f , .8f ) , Random.Range ( .3f , .8f ) , Random.Range ( .3f , .8f ) );
    }

    public void ChildToTransform( Transform newParent ) {
        SpriteRenderer sr = GetComponent<SpriteRenderer> ();
        transform.position = newParent.transform.position - sr.transform.up * sr.sprite.bounds.size.y;
        transform.parent = newParent;
    }

    public void AddPointToScore() {
        gameManager.AddPoints (1);
        Destroy ( gameObject );
    }
}
