using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

   

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResumeGame() {
        GameManager._instance.gameIsPaused = false;
        GameManager._instance.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame() { Application.Quit(); }


  

}
