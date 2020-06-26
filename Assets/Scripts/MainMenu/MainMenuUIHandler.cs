using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    Button playButton;
    Button upgradeWindowButton;

    UpgradeWindowUIHandler upgradeWindowUIHandler;

    PlayerData playerData;
    AsyncOperation async;

    private void Awake() {
        playerData = SaveSystem.LoadPlayer ();
        Initialize ();
    }

    private void Start() {
        StartCoroutine ( LoadSceneASync () );
    }

    private void Initialize() {
        upgradeWindowUIHandler = GetComponentInChildren<UpgradeWindowUIHandler> ();
        upgradeWindowUIHandler.Initialize ( playerData );

        Button [] buttonsInChildren = GetComponentsInChildren<Button> ();
        playButton = buttonsInChildren [ 0 ];
        upgradeWindowButton = buttonsInChildren [ 1 ];

        playButton.onClick.AddListener ( () => {
            async.allowSceneActivation = true;
            //ApplicationManager.LoadScene ( "GameScene" );
        } );

        upgradeWindowButton.onClick.AddListener ( () => {
            upgradeWindowUIHandler.Show ();
        } );
        upgradeWindowUIHandler.Hide ();
    }

    IEnumerator LoadSceneASync() {
        async = SceneManager.LoadSceneAsync ( 1 );
        async.allowSceneActivation = false;

        while ( !async.isDone ) {
            yield return null;
        }
    }
}
