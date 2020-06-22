using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public WhenToDetstroy whenToDestroy;
    public enum WhenToDetstroy
    {
        ENTER,
        EXIT
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( whenToDestroy != WhenToDetstroy.ENTER ) {
            return;
        }

        if ( CheckForDestroyable ( collision ) ) {
            Destroy ( collision.gameObject );
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if ( whenToDestroy != WhenToDetstroy.EXIT ) {
            return;
        }

        if ( CheckForDestroyable ( collision ) ) {
            Destroy ( collision.gameObject );
        }
    }

    private bool CheckForDestroyable( Collider2D collision ) {
        if ( collision.GetComponent<IDestroyable> () != null )
            return true;
        return false;
    }
}
