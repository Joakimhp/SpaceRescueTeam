using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour, IDamagable
{
    private int originalSize;
    private int size;
    private Vector2 direction;
    private float speed = 40f;

    Rigidbody2D rb;
    GameObject meteorPrefab;

    bool isImmune;

    public void Initialize( int originalSize , int size , Vector2 direction , GameObject meteorPrefab ) {
        this.originalSize = originalSize;
        this.size = size;
        this.direction = direction;
        this.meteorPrefab = meteorPrefab;

        transform.localScale = new Vector3 ( size , size , size );

        rb = GetComponent<Rigidbody2D> ();

        isImmune = true;
        StartCoroutine ( ImmunityTimer () );

        UpdateVelocity ();
    }

    private void UpdateVelocity() {
        Vector2 newVelocity = direction * speed * Time.deltaTime;
        rb.velocity = newVelocity;
    }

    private void AddDirection( Vector2 newDirection ) {
        Debug.Log ( "From direction: " + direction );
        direction += newDirection;
        //direction = new Vector2 ( direction.x + newDirection.x , direction.y + newDirection.y );
        direction = direction.normalized;
        Debug.Log ( "To direction: " + direction );
        UpdateVelocity ();
    }

    public void TakeDamage( int damage ) {
        int newMeteorCount = size - damage;
        if ( newMeteorCount > 0 ) {
            for ( int i = 0; i <= newMeteorCount; i++ ) {
                Vector2 newDirection = Random.insideUnitCircle.normalized;
                Meteor newMeteor = Instantiate ( gameObject , transform.position , Quaternion.identity ).GetComponent<Meteor> ();
                newMeteor.Initialize ( originalSize , size - 1 , newDirection , meteorPrefab );
            }
        }

        Destroy ( gameObject );
    }

    private void ImpactWithOtherMeteor( Meteor otherMeteor ) {
        otherMeteor.TakeDamage ( size );
        TakeDamage ( otherMeteor.size );
    }

    //private void OnTriggerStay2D( Collider2D collision ) {
    //    if ( collision.tag == "Meteor" ) {
    //        if ( isImmune ) {
    //            Vector2 newDirection = ( transform.position - collision.transform.position ).normalized;
    //            AddDirection ( newDirection );
    //            return;
    //        } else {
    //            Meteor otherMeteor = collision.GetComponent<Meteor> ();
    //            ImpactWithOtherMeteor ( otherMeteor );
    //            return;
    //        }
    //    }
    //    Destroy ( gameObject );
    //}

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.GetComponent<Shot> () != null ) {
            return;
        }

        if ( collision.tag == "Meteor" ) {
            if ( isImmune ) {
                Vector2 newDirection = ( transform.position - collision.transform.position ).normalized;
                AddDirection ( newDirection );
                return;
            } else {
                Meteor otherMeteor = collision.GetComponent<Meteor> ();
                ImpactWithOtherMeteor ( otherMeteor );
            }

        }

        if ( collision.tag == "Player" ) {
            collision.GetComponent<PlayerController> ().TakeDamage ( 1 );
        }

        Destroy ( gameObject );
    }

    IEnumerator ImmunityTimer() {
        float timer = 1.5f + Time.deltaTime;

        while ( timer > 0 ) {
            timer -= Time.deltaTime;
            yield return null;
        }

        isImmune = false;
    }
}
