using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    Button playButton;
    Button upgradeWindowButton;

    UpgradeWindowUIHandler upgradeWindowUIHandler;

    PlayerData playerData;

    private void Awake() {
        playerData = SaveSystem.LoadPlayer ();

        Initialize ();
    }

    private void Initialize() {
        upgradeWindowUIHandler = GetComponentInChildren<UpgradeWindowUIHandler> ();
        upgradeWindowUIHandler.Initialize ( playerData );

        Button [] buttonsInChildren = GetComponentsInChildren<Button> ();
        playButton = buttonsInChildren [ 0 ];
        upgradeWindowButton = buttonsInChildren [ 1 ];

        playButton.onClick.AddListener ( () => {
            ApplicationManager.LoadScene ( "GameScene" );
        } );

        upgradeWindowButton.onClick.AddListener ( () => {
            upgradeWindowUIHandler.Show();
        } );
        upgradeWindowUIHandler.Hide ();

        
    }
}
