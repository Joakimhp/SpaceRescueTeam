using System.Collections;
using System.Collections.Generic;
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

    float cooldown = .3f;
    float cooldownTimer;
    public void Initialize( Sprite sprite , float shotSpeedLevel ) {
        shotSprite = sprite;
        shotPrefab = Resources.Load<GameObject> ( "ShotPrefab" );
        shotSpeed = shotBaseSpeed + ( shotSpeedModifier * shotSpeedLevel );

        gunSprite = GetComponent<SpriteRenderer> ();
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
        while ( cooldownTimer > 0f ) {
            gunSprite.color = Vector4.Lerp ( Color.white , Color.red , cooldownTimer / cooldown );
            cooldownTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame ();
        }
        cooldownTimer = 0f;
    }
}
