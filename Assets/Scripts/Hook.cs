using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : Tool
{
    Vector3 direction;
    HookController hookController;
    LineRenderer lineRenderer;

    GameObject hookHead;

    public void Initialize() {
        hookController = GetComponentInChildren<HookController> ();
        hookController.Initialize ();
        hookHead = hookController.gameObject;

        lineRenderer = GetComponent<LineRenderer> ();
        lineRenderer.enabled = false;
    }

    public override void UseTool() {
        direction = -transform.up;
        hookController.gameObject.SetActive ( true );
        hookController.SendHookInDirection ( direction );

        StartCoroutine ( UpdateHookLineRenderer () );
    }

    IEnumerator UpdateHookLineRenderer() {
        if(lineRenderer.positionCount != 2 ) {
            lineRenderer.positionCount = 2;
        }

        lineRenderer.enabled = true;
        while ( hookController.gameObject.activeSelf == true ) {
            lineRenderer.SetPosition ( 0 , transform.position + transform.up*.2f);
            lineRenderer.SetPosition ( 1 , hookHead.transform.position + hookHead.transform.up * .15f);
            yield return new WaitForEndOfFrame ();
        }
        lineRenderer.enabled = false;
    }
}
