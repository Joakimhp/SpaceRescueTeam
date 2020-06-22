﻿using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Gun : Tool
{
    private float shotBaseSpeed = 1f;
    private float shotSpeedModifier = 1.05f;
    private float shotSpeed;
    private SpriteRenderer gunSprite;
    private Sprite shotSprite;
    private GameObject shotPrefab;

    private float cooldown = .3f;
    private float cooldownTimer;
    public void Initialize( Sprite sprite , float shotSpeedLevel ) {
        shotSprite = sprite;
        shotPrefab = Resources.Load<GameObject> ( "ShotPrefab" );
        shotSpeed = shotBaseSpeed + ( shotSpeedModifier * shotSpeedLevel );

        //gunSprite = GetComponent<SpriteRenderer> ();
        SpriteRenderer [] sprts = GetComponentsInChildren<SpriteRenderer> ();
        gunSprite = sprts [ 0 ];
        Debug.Log ( gunSprite.gameObject.name );
    }

    public override void UseTool() {
        if ( cooldownTimer == 0f ) {
            Shot shot = Instantiate ( shotPrefab , transform.position , transform.rotation ).GetComponent<Shot> ();
            shot.Initialize ( shotSprite , shotSpeed );
            shot.Shoot ();
            StartCoroutine ( StartCooldownTimer () );
        }
    }

    IEnumerator StartCooldownTimer() {
        cooldownTimer = cooldown;
        Debug.Log ( "Cooldown" );
        while ( cooldownTimer > 0f ) {
            Vector4 newColor = Vector4.Lerp ( Color.white , Color.red , cooldownTimer / cooldown );
            gunSprite.color = newColor;
            Debug.Log ( gunSprite.color );
            cooldownTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame ();
        }
        Debug.Log ( "Cooled down" );
        cooldownTimer = 0f;
    }
}
