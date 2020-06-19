using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer> ();

        spriteRenderer.color = new Color ( Random.Range ( 0f , 1f ) , Random.Range ( 0f , 1f ) , Random.Range ( 0f , 1f ) );
    }

    public void ChildToTransform(Transform newParent) {
        transform.parent = newParent;
        transform.localPosition = Vector3.zero;
    }

    public void AddPointToScore() {
        //Add points to score here;
        Destroy ( gameObject );
    }
}
