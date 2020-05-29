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
        transform.localRotation= originalRotation;
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
            ResetHook ();
        }
        if ( hookState == HookState.IN_USE && collision.gameObject.name != "Player" ) {
            StopAllCoroutines ();
            StartCoroutine ( ReturnHook () );
        }
    }

    IEnumerator MoveHookInDirection( Vector3 direction ) {
        transform.parent = null;
        hookState = HookState.IN_USE;
        spriteRenderer.sprite = hookImages [ 1 ];
        while ( true ) {
            Vector3 newPos = transform.position + direction * speed * Time.deltaTime;
            transform.position = newPos;
            if ( Input.GetKeyDown ( KeyCode.Backspace ) ) {
                break;
            }
            yield return new WaitForEndOfFrame ();
        }
    }

    IEnumerator ReturnHook() {
        hookState = HookState.RETURNING;
        spriteRenderer.sprite = hookImages [ 0 ];
        while ( hookState == HookState.RETURNING ) {
            Vector3 direction = originalParent.position - transform.position;
            direction = direction.normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
            transform.position = newPosition;
            yield return new WaitForEndOfFrame ();
        }
    }
}
