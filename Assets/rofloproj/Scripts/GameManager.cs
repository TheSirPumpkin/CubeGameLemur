using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject GameWonPopup;
    public JoystickMove JoystickMove;
    public RespawnController RespawnController;
    public GameObject[] PlatformPrefab;
    public GameObject[] EndPlatformPrefab;
    public GameObject[] StartPlatformPrefab;
    public GameObject StartPlatform;
    public GameObject EndPlatform;
    public Vector3 PlatformStartPosition;
    public int LevelsLimit;
    public bool FirstDeath;
    public UnityEngine.UI.Text CurrentLevelText;
    public UnityEngine.UI.Text CurrentLevelTextMenu;
    public PauseMenu PauseMenu;

    private bool gameOver;
    private bool scoreSet;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        if (PlayerPrefs.GetInt("LevelUi") == 0)
        {
            PlayerPrefs.SetInt("LevelUi", 1);
        }
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        if (PlayerPrefs.GetInt("Level") >= LevelsLimit)
        {
            PlayerPrefs.SetInt("Level", LevelsLimit);
        }



        PlayerPrefs.SetFloat("MagnetTime", 3f);
        PlayerPrefs.SetFloat("CrushTime", 3f);
        PlayerPrefs.SetFloat("ShieldTime", 3f);


        CurrentLevelTextMenu.text = CurrentLevelText.text = "Level-" + PlayerPrefs.GetInt("LevelUi");
        GenerateLevel();
    }
    private void Start()
    {
        RenderSettings.fogColor = GrowableObject.Instance.CurrentColor;
    }
    private void Update()
    {
        if (JoystickMove.rb.Count < 1)
        {
            Time.timeScale = 1;
            if (gameOver)
            {
                return;
            }
            gameOver = true;

            SetScoreBeforeDeath();

        }
        //if (JoystickMove.rb.Count == 1 && Time.timeScale != 0 && JoystickMove.rb[0].gameObject.activeSelf == false)
        //{
        //    JoystickMove.rb[0].gameObject.SetActive(true);
        //    PauseMenu?.gameObject.SetActive(true);
        //    PauseMenu?.PreGame();
        //}
     
    }
    private void SetScoreBeforeDeath()
    {
        // if (scoreSet)
        // {
        //     return;
        // }
        SetBestScore();
        SendScoreAndDie();
    }

    private void SendScoreAndDie()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            try
            {
                Social.ReportScore(PlayerPrefs.GetInt("BestScore"), GPGSIds.leaderboard_top_scores, (bool success) =>
                {
                });
                CancelInvoke();

                Invoke("DeathDelay", 2f);

                scoreSet = true;
            }

            catch
            {
                CancelInvoke();
                Invoke("DeathDelay", 2f);
                scoreSet = true;
            }
        }
        else
        {
            CancelInvoke();
            Invoke("DeathDelay", 2f);
        }
        scoreSet = true;
        Destroy(PauseMenu.gameObject);
    }

    private bool SetBestScore()
    {
        if (PlayerPrefs.GetInt("BestScore") < PointManager.Instance.Bestscore.scoreBestValue * PointManager.Instance.ScoreMultiplierValue)
        {
            PlayerPrefs.SetInt("BestScore", PointManager.Instance.Bestscore.scoreBestValue * PointManager.Instance.ScoreMultiplierValue);
            //PointManager.Instance.Bestscore.gameObject.SetActive(true);
            return true;
        }
        else return false;
    }
    public void SendScoreAndWin()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            try
            {
                Social.ReportScore(PlayerPrefs.GetInt("BestScore"), GPGSIds.leaderboard_top_scores, (bool success) =>
                {
                });
            }

            catch
            {
            }
        }
    }
    public void DeathDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GenerateLevel()
    {
        int platformCount = 0;
        List<GameObject> platfromList = new List<GameObject>();
        foreach (var platfrom in PlatformPrefab)
        {
            if (platfrom.GetComponent<PlatformObstacleGeneraotr>().StartsOn <= PlayerPrefs.GetInt("Level"))
            {
                platfromList.Add(platfrom);
            }
        }
        StartPlatform = Instantiate(StartPlatformPrefab[Random.Range(0, StartPlatformPrefab.Length)], PlatformStartPosition, Quaternion.identity);
        for (int i = 1; i < PlayerPrefs.GetInt("Level");)
        {
            if (i < PlayerPrefs.GetInt("Level"))
            {
                Instantiate(platfromList[Random.Range(0, platfromList.Count)], PlatformStartPosition + new Vector3(PlatformStartPosition.x * 4 * i, 0, 0), Quaternion.identity);
            }
            i++;
            platformCount = i;
        }
        EndPlatform = Instantiate(EndPlatformPrefab[Random.Range(0, EndPlatformPrefab.Length)], PlatformStartPosition + new Vector3(PlatformStartPosition.x * 4 * platformCount, 0, 0), Quaternion.identity);
    }

    public void EnableNextLevelPopup()
    {
        Time.timeScale = 0;
        bool bestScore = SetBestScore();
        GameWonPopup.SetActive(true);
        GameWonPopup.GetComponent<GameWinPopup>().SetTexts((PlayerPrefs.GetInt("LevelUi") - 1).ToString(),
            GetComponent<ProgressBarController>().CubesText.text,
            PointManager.Instance.ScoreMultiplierValue, PointManager.Instance.scoreCounter.scoreValue * PointManager.Instance.ScoreMultiplierValue,
            bestScore);
        PauseMenu.joystick.SetActive(false);
        SendScoreAndWin();

        GoogleMobileAdsManager.Instance.ShowInterstitial();
        //GoogleMobileAdsManager.Instance.RequestBanner();
    }
}
