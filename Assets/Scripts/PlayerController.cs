using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Hook hook;

    private Rigidbody2D rb;

    public float speed;
    public float angularSpeed;

    private void Awake() {
        hook = GetComponentInChildren<Hook> ();
        hook.Initialize ();

        rb = GetComponent<Rigidbody2D> ();
    }

    private void Update() {
        if ( Input.GetKeyDown ( KeyCode.Space ) ) {
            hook.UseTool ();
        }

        float inputSpeed = Input.GetAxisRaw ( "Vertical" );
        if ( inputSpeed == 0 ) {
            Vector3 dampenedVelocity = rb.velocity * -1f * .1f;
            rb.AddForce ( dampenedVelocity );
            //inputSpeed = -1f * rb.velocity.magnitude * .2f;
        } else {
            if ( inputSpeed < 0 ) {
                inputSpeed /= 2;
            }

            rb.AddForce ( transform.up * inputSpeed * speed * Time.deltaTime );
        }

            //float inputSpeed = Input.GetAxisRaw ( "Vertical" );
            //inputSpeed = inputSpeed < 0 ? inputSpeed / 2 : inputSpeed;

            float torqueToAdd = Input.GetAxisRaw ( "Horizontal" ) * angularSpeed * Time.deltaTime;
            rb.AddTorque ( torqueToAdd );
        }
    }
