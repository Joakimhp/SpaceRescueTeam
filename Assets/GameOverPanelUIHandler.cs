using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class GameOverPanelUIHandler : UIWindowBehaviour
{
    TextMeshProUGUI title;
    Animator animator;
    public void Initialize() {
        animator = GetComponent<Animator> ();

        TextMeshProUGUI [] tmpsTexts = GetComponentsInChildren<TextMeshProUGUI> ();
        title = tmpsTexts [ 0 ];
    }

    private void Update() {
        if ( Input.GetKeyDown ( KeyCode.Space ) ) {
            Hide ();
        }else if ( Input.GetKeyDown ( KeyCode.LeftShift ) ) {
            Show ();
        }
    }

    public override void Hide() {
        if ( animator != null ) {
            animator.Play ( "Base Layer.MediumPanelHide" );

        }
    }

    public override void Show() {
        if ( animator != null ) {
            animator.Play ( "Base Layer.MediumPanelShow" );
        }
    }

    public void ShowWinPanel() {
        title.text = "You win!";
        Show ();
    }
    public void ShowLosePanel() {
        title.text = "Try again?";
        Show ();
    }

    public override void UpdateUI() {
        throw new System.NotImplementedException ();
    }
}
