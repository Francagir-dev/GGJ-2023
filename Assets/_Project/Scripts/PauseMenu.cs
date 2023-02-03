using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void RestartGame() {
        GameManager._instance.gameIsPaused = false;
        GameManager._instance.pauseMenu.SetActive(false);
        GameManager._instance.gameTimer = 0.0f;
        Time.timeScale = 1f;
    }
    public void ResumeGame() {
        GameManager._instance.gameIsPaused = false;
        GameManager._instance.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame() { Application.Quit(); }

}
