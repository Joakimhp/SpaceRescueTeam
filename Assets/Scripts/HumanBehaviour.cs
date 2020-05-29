using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer> ();

        spriteRenderer.color = new Color ( Random.Range ( 0f , 1f ) , Random.Range ( 0f , 1f ) , Random.Range ( 0f , 1f ) );
    }
}
