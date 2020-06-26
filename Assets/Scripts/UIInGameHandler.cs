using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIInGameHandler : MonoBehaviour
{
    UIHealthOverviewUIHandler UIHealthOverviewUIHandler;

    ScoreOverviewUIHandler scoreOverviewUIHandler;

    GameOverPanelUIHandler gameOverPanelUIHandler;

    public void Initialize(PlayerController player, List<Transform> npcTransforms) {
        UIHealthOverviewUIHandler = GetComponentInChildren<UIHealthOverviewUIHandler> ();
        UIHealthOverviewUIHandler.Initialize (player);

        scoreOverviewUIHandler = GetComponentInChildren<ScoreOverviewUIHandler> ();
        scoreOverviewUIHandler.Initialize ( npcTransforms );

        gameOverPanelUIHandler = GetComponentInChildren<GameOverPanelUIHandler> ();
        gameOverPanelUIHandler.Initialize ();
    }

    public void ShowGameOverScreen(bool playerWon) {
        if ( playerWon ) {
            gameOverPanelUIHandler.ShowWinPanel ();
        } else {
            gameOverPanelUIHandler.ShowLosePanel ();
        }
    }

    public void UpdateUI(int score) {
        UIHealthOverviewUIHandler.UpdateUI ();
        scoreOverviewUIHandler.UpdateUI (score);
    }
}
