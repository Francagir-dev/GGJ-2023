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
        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenu();


    }
    private void FixedUpdate()
    {
        Counter();
    }

    /// <summary>
    /// Timer sums time and change text
    /// </summary>   
    void Counter()
    {

        int minutes = 0;
        int seconds = 0;

        string minutesString = "";
        string secondsString = "";

        gameTimer += Time.deltaTime; //Sum time

        minutes = (int)(gameTimer / 60);
        seconds = (int)(gameTimer % 60);



        //Formatting Seconds
        if (seconds < 10) secondsString = "0" + seconds.ToString();
        else secondsString = seconds.ToString();

        //Formatting Minutes
        if (minutes > 9) minutesString = minutes.ToString();           
        else  minutesString = "0" + minutes.ToString();
            
         //Setting text
        timerText.text = minutesString + ":" + secondsString;
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

