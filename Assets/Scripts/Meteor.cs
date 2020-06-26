using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour, IDamagable, IDestroyable
{
    private int originalSize;
    private int size;
    private Vector2 direction;
    private float speed = 40f;
    [SerializeField]
    private float maxRandomTorque;

    Rigidbody2D rb;
    GameObject meteorPrefab;

    bool isImmune = false;

    private bool waitingToBeDestroyed = false;

    public void Initialize( int originalSize , int size , Vector2 direction , GameObject meteorPrefab ) {
        this.originalSize = originalSize;
        this.size = size;
        this.direction = direction;
        this.meteorPrefab = meteorPrefab;

        transform.localScale = new Vector3 ( size , size , size );

        rb = GetComponent<Rigidbody2D> ();

        StartCoroutine ( ImmunityTimer () );

        UpdateVelocity ();
        ApplyRandomRotation ();
    }

    private void ApplyRandomRotation() {
        float torqueToAdd = Random.Range ( -maxRandomTorque , maxRandomTorque );
        rb.AddTorque ( torqueToAdd );
    }

    private void UpdateVelocity() {
        Vector2 newVelocity = direction * speed * Time.deltaTime;
        rb.velocity = newVelocity;
    }

    private void AddDirection( Vector2 newDirection ) {
        direction += newDirection;
        //direction = new Vector2 ( direction.x + newDirection.x , direction.y + newDirection.y );
        direction = direction.normalized;
        UpdateVelocity ();
    }

    public void TakeDamage( int damage ) {
        if ( waitingToBeDestroyed ) {
            return;
        }
        
        int newMeteorCount = size - damage;
        if ( newMeteorCount > 0 ) {
            for ( int i = 0; i <= newMeteorCount; i++ ) {
                Meteor newMeteor = Instantiate ( meteorPrefab , transform.position , Quaternion.identity ).GetComponent<Meteor> ();
                Vector2 newDirection = Random.insideUnitCircle.normalized;
                if ( !newMeteor.GetComponent<Collider2D> ().enabled )
                    Debug.Log ( "It's a me" );
                newMeteor.Initialize ( originalSize , size - 1 , newDirection , meteorPrefab );
            }
        }

        waitingToBeDestroyed = true ;
        Destroy ( gameObject );
    }

    private void ImpactWithOtherMeteor( Meteor otherMeteor ) {
        otherMeteor.TakeDamage ( 2 );
        TakeDamage ( 2 );
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
            PlayerController playerController = collision.GetComponent<PlayerController> ();
            playerController.TakeDamage ( 1 );
            playerController.AddForceAwayFromPoint ( transform.position );
            TakeDamage ( 1 );
        }

        if ( collision.tag == "Environment" ) {
            Destroy ( gameObject );
        }
    }

    IEnumerator ImmunityTimer() {
        isImmune = true;
        float timer = 1.5f + Time.deltaTime;

        while ( timer > 0 ) {
            timer -= Time.deltaTime;
            yield return null;
        }

        isImmune = false;
    }
}
