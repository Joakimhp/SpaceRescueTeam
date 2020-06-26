using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HookController : MonoBehaviour
{
    private float speed = 5f;

    //private bool canSendHook = true;
    //private bool hookIsReturningToPlayer = false;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    [SerializeField]
    private Sprite [] hookImages;

    SpriteRenderer spriteRenderer;

    private enum HookState
    {
        DOCKED,
        IN_USE,
        RETURNING
    }

    private HookState hookState = HookState.DOCKED;

    public void Initialize() {
        spriteRenderer = GetComponent<SpriteRenderer> ();

        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;

        gameObject.SetActive ( false );
    }

    private void ResetHook() {
        StopAllCoroutines ();

        transform.parent = originalParent;
        transform.localRotation = originalRotation;
        transform.localPosition = originalPosition;

        hookState = HookState.DOCKED;

        gameObject.SetActive ( false );
    }

    public void SendHookInDirection( Vector3 direction ) {
        if ( hookState == HookState.DOCKED ) {
            StartCoroutine ( MoveHookInDirection ( direction ) );
        }
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( hookState == HookState.RETURNING && collision.gameObject.tag == "Player" ) {
            foreach ( Transform child in transform ) {
                NPCController npc = child.GetComponent<NPCController> ();
                if ( npc != null ) {
                    npc.AddPointToScore ();
                }
            }

            ResetHook ();
        }
        if ( hookState == HookState.IN_USE ) {
            //Debug.Log ( "I've hit: " + collision.gameObject.name );
            //if ( collision.gameObject.tag == "NPC" ) {
            //    collision.GetComponent<NPCController> ().ChildToTransform ( transform );
            //} else if ( collision.gameObject.tag != "Player" && collision.GetComponent<ObjectDestroyer>() == null ) {
            //    StopAllCoroutines ();
            //    StartCoroutine ( ReturnHook () );
            //}

            if ( collision.tag != "Player" && collision.GetComponent<ObjectDestroyer> () == null ) {
                if ( collision.gameObject.tag == "NPC" ) {
                    collision.GetComponent<NPCController> ().ChildToTransform ( transform );
                }
                StopAllCoroutines ();
                StartCoroutine ( ReturnHook () );
            }
        }
    }

    IEnumerator MoveHookInDirection( Vector3 direction ) {
        transform.parent = null;
        hookState = HookState.IN_USE;
        SetHookSprite ( true );
        while ( hookState == HookState.IN_USE ) {
            Vector3 newPos = transform.position + direction * speed * Time.deltaTime;
            transform.position = newPos;

            yield return new WaitForEndOfFrame ();
        }
    }

    IEnumerator ReturnHook() {
        hookState = HookState.RETURNING;
        SetHookSprite ( false );
        while ( hookState == HookState.RETURNING ) {
            Vector3 direction = originalParent.position - transform.position;
            direction = direction.normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
            transform.position = newPosition;

            yield return new WaitForEndOfFrame ();
        }
    }

    private void SetHookSprite( bool openHook ) {
        if ( openHook ) {
            spriteRenderer.sprite = hookImages [ 1 ];
        } else {
            spriteRenderer.sprite = hookImages [ 0 ];
        }
    }
}
