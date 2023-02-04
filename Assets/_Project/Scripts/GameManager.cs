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
    public bool gameIsPaused = false;

    [Header("Experience")]
    public int actualLevel = 0;
    public int actualExpPoints = 0;
    public int expPointsLevelUP = 10;
    public ProgressBar expProgressBar;
    public int rootsGrabbed = 0;

    public int maxRootsInScreen = 300;

    [Header("Upgrades")]
    public GameObject parentPlayerUpgrades;
    public GameObject parentUpgradesForSelect;
    public GameObject prefabCard;
    public GameObject prefabIcon;

    public List<Upgrade> allUpgrades = new List<Upgrade>();
    public List<Upgrade> playerUpgrades = new List<Upgrade>();
    private List<Upgrade> showUpgradesList = new List<Upgrade>();

    private Upgrade randomizedUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
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
        CheckEXP();
    }

    #region Timer
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
        else minutesString = "0" + minutes.ToString();

        //Setting text
        timerText.text = minutesString + ":" + secondsString;
    }
    #endregion

    #region Pause
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
    #endregion

    #region EXP
    public void CheckEXP()
    {
        //if I have more exp than the requiered
        if (actualExpPoints > expPointsLevelUP)
        {
            actualExpPoints = expPointsLevelUP - actualExpPoints; //save extra
            LevelUP();
        }
        else if (actualExpPoints == expPointsLevelUP)
        {
            actualExpPoints = 0;
            LevelUP();
        }
        expProgressBar.current = actualExpPoints;
    }
    public void LevelUP()
    {
        expProgressBar.maximum = (int)(expProgressBar.maximum * 1.5f);  //Add more exp need to lvl up
        expPointsLevelUP = expProgressBar.maximum;
        actualLevel++; //level Up
        SelectUpgrades();


    }
    public void AddExp(int expToSum)
    {
        actualExpPoints += expToSum;
    }

    #endregion

    #region Upgrades

    public void ShowCards(Upgrade upgradeToDisplay)
    {
        GameObject upgrade = Instantiate(prefabCard, Vector3.zero, Quaternion.identity);
        upgrade.GetComponent<UpgradeDisplay>().upgrade = upgradeToDisplay;
    }

    void SelectUpgrades()
    {
        for (int i = 0; i < 3; i++)
        {
            CheckUpgrade();

        }      
    }


    public Upgrade RandomUpgrade()
    {
        return allUpgrades[Random.Range(0, allUpgrades.Count)];
    }
    public void BoostUpgrade()
    {
        playerUpgrades[playerUpgrades.IndexOf(randomizedUpgrade)].level = playerUpgrades[playerUpgrades.IndexOf(randomizedUpgrade)].level++;
    }

    public void CheckUpgrade()
    {
        randomizedUpgrade = RandomUpgrade();
        for (int i = 0; i < playerUpgrades.Count; i++)
        {
            if (playerUpgrades[i].upgradeType.Equals("Tool") && randomizedUpgrade.upgradeType.Equals("Tool"))
            {
                if (playerUpgrades[i].name.Equals(randomizedUpgrade.name))
                {
                    showUpgradesList.Add(randomizedUpgrade);
                    ShowCards(randomizedUpgrade);
                }
                else
                {
                    CheckUpgrade();
                }
            }
            else {
                showUpgradesList.Add(randomizedUpgrade);
                ShowCards(randomizedUpgrade);
            }
        }
        #endregion
    }
}

public struct PlayerInfo
{

    string name;
    float durationRun;
    long pointsRun;


}

