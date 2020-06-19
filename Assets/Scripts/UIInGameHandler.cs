using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameHandler : MonoBehaviour
{
    UIHealthOverviewUIHandler UIHealthOverviewUIHandler;

    GameOverPanelUIHandler gameOverPanelUIHandler;

    public void Initialize(PlayerController player) {
        UIHealthOverviewUIHandler = GetComponentInChildren<UIHealthOverviewUIHandler> ();
        UIHealthOverviewUIHandler.Initialize (player);

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

    public void UpdateUI() {
        UIHealthOverviewUIHandler.UpdateUI ();
    }
}
