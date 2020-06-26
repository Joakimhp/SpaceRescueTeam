using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private UIInGameHandler uiInGameHandler;

    private MeteorSpawner meteorSpawner;
    private NPCSpawner npcSpawner;

    private PlatformSpawner platformSpawner;

    private ScoreKeeper scoreKeeper;

    private Transform [] environmentTransforms;

    private void Awake() {
        GameObject playerPrefab = Resources.Load<GameObject> ( "Player" );
        Vector3 spawnPosition;
        player = Instantiate ( playerPrefab , Vector3.zero , Quaternion.identity ).GetComponent<PlayerController> ();

        scoreKeeper = new ScoreKeeper ();

        InitializeComponentsInChildren ();
    }

    private void InitializeComponentsInChildren() {
        environmentTransforms = GetComponentsInChildren<Transform> ();
        SpriteRenderer groundSprite = environmentTransforms [ environmentTransforms.Length - 2 ].GetComponent<SpriteRenderer> ();

        npcSpawner = GetComponentInChildren<NPCSpawner> ();
        npcSpawner.Initialize ( 3 , groundSprite );
        List<Transform> npcTransforms = npcSpawner.GetNPCTransforms ();

        platformSpawner = GetComponentInChildren<PlatformSpawner> ();
        platformSpawner.Initialize ( npcTransforms , groundSprite );


        player.Initialize ( this , SaveSystem.LoadPlayer () );
        player.transform.position = platformSpawner.GetPlayerSpawnPoint ();
        meteorSpawner = GetComponentInChildren<MeteorSpawner> ();
        meteorSpawner.Initialize ();

        uiInGameHandler = GetComponentInChildren<UIInGameHandler> ();
        uiInGameHandler.Initialize ( player , npcTransforms );

        NPCController [] npcControllers = FindObjectsOfType<NPCController> ();
        foreach ( NPCController npc in npcControllers ) {
            npc.Initialize (this);
        }
    }

    public void AddPoints(int pointstoAdd ) {
        scoreKeeper.BumpScore ( pointstoAdd );
        UpdateUI ();
        if ( CheckForGameOver () ) {
            platformSpawner.ActivateEndBeam();
        }
    }

    public void GameOver( bool playerWon ) {
        UpdateUI ();
        uiInGameHandler.ShowGameOverScreen ( playerWon );
    }

    private bool CheckForGameOver() {
        if (scoreKeeper.GetScore() >= npcSpawner.GetAmountOfNPCs () ) {
            return true;
        }
        return false;
    }
    
    public void UpdateUI() {
        uiInGameHandler.UpdateUI (scoreKeeper.GetScore());
    }
}

public class ScoreKeeper
{
    public int score;

    public ScoreKeeper() {
        score = 0;
    }

    public int GetScore() {
        return score;
    }

    public void BumpScore(int bumpAmount) {
        score += bumpAmount;
    }
}