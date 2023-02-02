using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }
    public void QuitGame() {
        Application.Quit(0);
    }
}
