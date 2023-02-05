using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] PauseMenu pauseGame;
    [SerializeField] TextMeshProUGUI intPoints;
    [SerializeField] TextMeshProUGUI duration;


    private void OnEnable()
    {
        Time.timeScale = 0f;
        intPoints.text = (GameManager._instance.rootsGrabbed * 10).ToString();
        duration.text = GameManager._instance.textTimer;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
