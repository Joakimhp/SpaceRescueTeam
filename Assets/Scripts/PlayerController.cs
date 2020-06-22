using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour, IDamagable
{
    private GameManager gameManager;
    private Hook hook;
    private Gun gun;

    private Rigidbody2D rb;

    public float speed;
    public float angularSpeed;

    private PlayerData playerData;

    public int health;
    private Sprite gunLevelSprite;

    [SerializeField]
    private Animator thrusterAnimator;
    private Animator spaceshipAnimator;

    public void Initialize( GameManager gameManager , PlayerData playerData ) {
        this.gameManager = gameManager;
        this.playerData = playerData;

        spaceshipAnimator = GetComponent<Animator> ();

        SOUpgrades upgrades = Resources.Load<SOUpgrades> ( "Upgrades" );
        health = playerData.upgradeLevels [ 0 ] + 1;
        foreach ( Upgrade upgrade in upgrades.upgrades ) {
            if ( upgrade.name == "Gun level" ) {
                gunLevelSprite = upgrade.upgradeDatas [ playerData.upgradeLevels [ 1 ] ].sprite;
            }
        }

        gun = GetComponentInChildren<Gun> ();
        gun.Initialize ( gunLevelSprite , playerData.upgradeLevels [ 2 ] + 1 );

        hook = GetComponentInChildren<Hook> ();
        hook.Initialize ();

        rb = GetComponent<Rigidbody2D> ();
    }

    private void Update() {
        if ( Input.GetKeyDown ( KeyCode.Q ) ) {
            hook.UseTool ();
        }

        if ( Input.GetKey ( KeyCode.E ) ) {
            gun.UseTool ();
        }

        Move ();
        Rotate ();
    }

    private void Move() {
        float inputSpeed = Input.GetAxisRaw ( "Vertical" );

        thrusterAnimator.SetBool ( "PlayerThrottles" , inputSpeed != 0 );
        thrusterAnimator.SetFloat ( "SpeedInput" , inputSpeed );

        if ( inputSpeed == 0 ) {
            Vector3 dampenedVelocity = rb.velocity * -1f * .1f;
            rb.AddForce ( dampenedVelocity );
        } else {
            if ( inputSpeed < 0 ) {
                inputSpeed /= 2;
            }

            rb.AddForce ( transform.up * inputSpeed * speed * Time.deltaTime );
        }
    }

    private void Rotate() {
        float torqueToAdd = Input.GetAxisRaw ( "Horizontal" ) * angularSpeed * Time.deltaTime;

        thrusterAnimator.SetBool ( "PlayerRotates" , torqueToAdd != 0 );
        thrusterAnimator.SetFloat ( "RotationInput" , torqueToAdd );

        rb.AddTorque ( torqueToAdd );
    }

    public void TakeDamage( int damage ) {
        health -= damage;
        if ( health <= 0 ) {
            gameManager.GameOver ( false );
            spaceshipAnimator.Play ( "Base Layer.DestroyPlayer" );
        } else {
            spaceshipAnimator.Play ( "Base Layer.PlayerTakeDamage" );
            gameManager.UpdateUI ();
        }
    }

    private void AnimationEventDestroyMe() {
        Destroy ( gameObject );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        //if ( collision.gameObject.tag == "Meteor" ) {
        //    TakeDamage ( 1 );
        //}
    }

}
