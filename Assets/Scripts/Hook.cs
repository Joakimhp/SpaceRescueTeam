using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : Tool
{
    Vector3 direction;
    HookController hookController;

    public void Initialize() {
        hookController = GetComponentInChildren<HookController> ();
        hookController.Initialize ();
    }

    public override void UseTool() {
        direction = -transform.up;
        hookController.gameObject.SetActive ( true );
        hookController.SendHookInDirection ( direction );
    }
}
