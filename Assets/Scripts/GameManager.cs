using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityScript.Macros;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private PlayerData playerData;
    private UIInGameHandler uiInGameHandler;

    private MeteorSpawner meteorSpawner;
    private NPCSpawner npcSpawner;

    private PlatformSpawner platformSpawner;

    private ScoreKeeper scoreKeeper;

    private Transform [] environmentTransforms;

    private void Awake() {
        GameObject playerPrefab = Resources.Load<GameObject> ( "Player" );
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


        playerData = SaveSystem.LoadPlayer ();
        player.Initialize ( this , playerData );
        player.transform.position = platformSpawner.GetPlayerSpawnPoint ();
        meteorSpawner = GetComponentInChildren<MeteorSpawner> ();
        meteorSpawner.Initialize ();

        uiInGameHandler = GetComponentInChildren<UIInGameHandler> ();
        uiInGameHandler.Initialize ( player , npcTransforms );

        NPCController [] npcControllers = FindObjectsOfType<NPCController> ();
        foreach ( NPCController npc in npcControllers ) {
            npc.Initialize ( this , player.playerData.upgradeLevels[1]);
        }
    }

    public void AddPointsAndGold( int pointstoAdd , int goldToAdd ) {
        scoreKeeper.BumpScore ( pointstoAdd );
        scoreKeeper.BumpGold ( goldToAdd );
        UpdateUI ();
        if ( CheckForGameOver () ) {
            platformSpawner.ActivateEndBeam ();
        }
    }

    public void GameOver( bool playerWon ) {
        UpdateUI ();
        if ( !playerWon ) {
            scoreKeeper.BumpGold ( -( scoreKeeper.GetGold() / 2 ) );
        }

        playerData.gold += scoreKeeper.GetGold ();
        SaveSystem.SavePlayer ( playerData );
        
        uiInGameHandler.ShowGameOverScreen ( playerWon , npcSpawner.GetAmountOfNPCs () , scoreKeeper.GetScore () , scoreKeeper.GetGold () );
    }

    private bool CheckForGameOver() {
        if ( scoreKeeper.GetScore () >= npcSpawner.GetAmountOfNPCs () ) {
            return true;
        }
        return false;
    }

    public void UpdateUI() {
        uiInGameHandler.UpdateUI ( scoreKeeper.GetScore () );
    }
}

public class ScoreKeeper
{
    private int score;
    private int goldEarned;

    public ScoreKeeper() {
        score = 0;
        goldEarned = 0;
    }

    public int GetScore() {
        return score;
    }

    public int GetGold() {
        return goldEarned;
    }

    public void BumpScore( int bumpAmount ) {
        score += bumpAmount;
    }

    public void BumpGold( int bumpAmount ) {
        if ( goldEarned + bumpAmount < 0 ) {
            goldEarned = 0;
        } else {
            goldEarned += bumpAmount;
        }
    }
}