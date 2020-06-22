using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.VR;

public class Shot : MonoBehaviour, IDestroyable
{
    SpriteRenderer spriteRenderer;
    float speed;

    public void Initialize( Sprite sprite , float speed ) {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        spriteRenderer.sprite = sprite;
        this.speed = speed;
    }

    public void Shoot() {
        StartCoroutine ( Move () );
    }

    IEnumerator Move() {
        while ( true ) {
            transform.position += ( transform.up * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame ();
        }
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.tag == "Meteor" ) {
            collision.GetComponent<Meteor> ().TakeDamage ( 1 );
            Destroy ( gameObject );
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if(collision.tag == "Environment" ) {
            Destroy ( gameObject );
        }
    }
}
