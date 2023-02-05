using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

   

    public void RestartGame() {
        GameManager._instance.gameIsPaused = false;
        GameManager._instance.pauseMenu.SetActive(false);       
        Time.timeScale = 1f;

        RestartAllStats();

    }
    public void ResumeGame() {
        GameManager._instance.gameIsPaused = false;
        GameManager._instance.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame() { Application.Quit(); }


    public void RestartAllStats() {
        GameManager._instance.gameTimer = 0.0f;
        GameManager._instance.expPointsLevelUP = 10;
        GameManager._instance.rootsGrabbed = 0;
        GameManager._instance.actualExpPoints = 0;
        GameManager._instance.expProgressBar.maximum = 10;
        GameManager._instance.playerUpgrades.Clear();
        GameManager._instance.showUpgradesList.Clear();

    }


}
