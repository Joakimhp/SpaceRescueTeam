using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class GameOverPanelUIHandler : UIWindowBehaviour
{
    TextMeshProUGUI title;
    Animator animator;

    Button [] gameOverButtons;

    public void Initialize() {
        animator = GetComponent<Animator> ();

        TextMeshProUGUI [] tmpsTexts = GetComponentsInChildren<TextMeshProUGUI> ();
        title = tmpsTexts [ 0 ];

        gameOverButtons = GetComponentsInChildren<Button> ();

        gameOverButtons [ 0 ].onClick.AddListener ( () => {
            ApplicationManager.LoadScene ( 0 );
        } );

        gameOverButtons [ 1 ].onClick.AddListener ( () => {
            ApplicationManager.ReloadScene ( SceneManager.GetActiveScene () );
        } );
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
