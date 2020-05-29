using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    SpriteRenderer sprite;

    private void Start() {
        sprite = GetComponent<SpriteRenderer> ();

        StartCoroutine ( ChangeColor () );
    }

    IEnumerator ChangeColor() {
        while ( true ) {
            float rnd = Random.Range ( 0f , 1f );
            sprite.color = new Color ( 1f, 1f, rnd );
            yield return new WaitForSeconds ( Random.Range ( 0f , 3f) );
        }
    }
}
