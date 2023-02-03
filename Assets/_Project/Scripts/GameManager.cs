using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager _instance;

    [Header("Timer")]
    public float gameTimer = 0.0f;
    [SerializeField] private TextMeshProUGUI timerText;


    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {   
        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) PauseMenu();     


    }
    private void FixedUpdate()
    {     
       GameCounterTimer();
    }

    /// <summary>
    /// Timer sums time and change text
    /// </summary>   
    void GameCounterTimer()
    {
        gameTimer += Time.deltaTime;
        
        timerText.text = gameTimer.ToString("F2");
    }
    public void PauseMenu()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
           pauseMenu.SetActive(true);
            Debug.Log("GameIsPaused");
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Debug.Log("GameIsResumed");
            Time.timeScale = 1.0f;
        }

    }

}

