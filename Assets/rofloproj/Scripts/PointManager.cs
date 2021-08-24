using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public delegate void OnPlayerGrowDelegate(int multiplier);
    public static OnPlayerGrowDelegate playerGrowDelegate;
    public delegate void OnObjectGrowDelegate(int multiplier);
    public static OnObjectGrowDelegate objectGrowDelegate;
    public delegate void OnPlayerDwonscaleDelegate(float multiplier);
    public static OnPlayerDwonscaleDelegate playerDownscaleDelegate;
    public delegate void OnChangeColor();
    public static OnChangeColor changeColor;
    public JoystickMove JoystickMove;
    public GameObject joystick;
    [SerializeField]
    public Color[] LevelColors;
    public GameObject PlayerPrefab;
    public static PointManager Instance { get; private set; }
    public ScoreCounter scoreCounter;
    public BestScore Bestscore;
    public PlayerMovement playerDeath;
    public GameObject Coin;
    public Transform Platfrom;
    public int ScoreMultiplierValue = 1;
    public float timer;
    public bool ScoreMultiplier;
    public GameObject multiplierIndicator;
    public GameObject score;
    public int CollectedPoints;
    public BombManager BombManager;
    public bool GameWon;
    private int collectToRise;
    private int levelpassed;
    private LookAtPlayer LookAtPlayer;

    public void Awake()
    {
        Instance = this;
        GetComponent<GrowableObject>().CurrentColor = LevelColors[Random.Range(0, LevelColors.Length)];
        LookAtPlayer = GetComponent<LookAtPlayer>();
       
    }
    void Start()
    {
        timer = 1f;
        multiplierIndicator.SetActive(false);
     
    }
    void Update()
    {
        if (GameWon)
        {
           LookAtPlayer.SetEndGameTarget(ScoreMultiplierValue);
        }
      
        if (ScoreMultiplier == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 1f;
                ScoreMultiplier = false;
                multiplierIndicator?.SetActive(false);
            }
        }

        foreach (var player in JoystickMove.rb)
        {
            if (player.GetComponent<PlayerMovement>().alive)
                return;
        }

    }
    public void AddScore(Vector3 position)
    {
        AddPlayer(position);
        //MMVibrationManager.Haptic(HapticTypes.Success);
        if (ScoreMultiplier == false)
        {
            CollectedPoints++;
            playerGrowDelegate?.Invoke(1);
            scoreCounter.scoreValue += 1 * PlayerPrefs.GetInt("LevelUi");
        }
        else if (ScoreMultiplier == true)
        {
            // SpawnCoin();

            CollectedPoints++;
            playerGrowDelegate?.Invoke(1);
            scoreCounter.scoreValue += 2 * PlayerPrefs.GetInt("LevelUi");
        }
        if (CollectedPoints > collectToRise)
        {
            collectToRise = CollectedPoints;
            // if (collectToRise % 5 == 0)
            // {
            //if (GrowableObject.Instance.TransfromToGrow.localScale.x / playerDeath.transform.localScale.x <= 1.5f)
            //{
            //    objectGrowDelegate?.Invoke((int)(10 * playerDeath.transform.localScale.x));
            //    levelpassed++;
            //    if (levelpassed == 5)
            //    {
            //        Random.seed = System.DateTime.Now.Millisecond;
            //        GrowableObject.Instance.CurrentColor = LevelColors[Random.Range(0, LevelColors.Length)];
            //        changeColor.Invoke();
            //        PlayerPrefs.SetInt("CleaeredLevels", PlayerPrefs.GetInt("CleaeredLevels") + 1);
            //        levelpassed = 0;
            //    }
            //}
            // }

            if (collectToRise % 30 == 0)
            {
                //BombManager.SpawnNewBomb();
            }
        }
        ScoreMultiplier = true;
        timer = 1f;
        multiplierIndicator.SetActive(true);
        StartCoroutine(X2Scale());
        StartCoroutine(ScoreScale());

        if (scoreCounter.scoreValue > Bestscore.scoreBestValue)
        {
            Bestscore.scoreBestValue = scoreCounter.scoreValue;
        }
    }

    private void SpawnCoin()
    {
        GameObject coin = Instantiate(Coin, GetRandomVector(), Quaternion.identity);
        coin.transform.rotation = Random.rotation;
    }

    private void AddPlayer(Vector3 position)
    {
        HapticManager.Instance.PlayPickuoHaptic();
        GameObject newPlayer = Instantiate(PlayerPrefab, position, Quaternion.identity);
        newPlayer.GetComponent<PlayerMovement>().joystick = this.joystick;
        JoystickMove.rb.Add(newPlayer.GetComponent<Rigidbody>());
        newPlayer.transform.parent = JoystickMove.transform;
        //newPlayer.transform.localPosition = GetRandomVectorPlayer(position);
    }

    private Vector3 GetRandomVectorPlayer(Vector3 position)
    {
        float randomX = UnityEngine.Random.Range(-0.1f + position.x, 0.1f + position.x);
        float randomY = position.y + 0.1f;
        float randomZ = UnityEngine.Random.Range(-0.1f + position.z, 0.1f + position.z);
        return new Vector3(randomX, randomY, randomZ);
    }

    private Vector3 GetRandomVector()
    {
        float randomX = UnityEngine.Random.Range(-3.1f * Platfrom.transform.localScale.x, 3.1f * Platfrom.transform.localScale.x);
        float randomY = UnityEngine.Random.Range(10 * Platfrom.transform.localScale.x, 20 * Platfrom.transform.localScale.x);
        float randomZ = UnityEngine.Random.Range(-3.1f * Platfrom.transform.localScale.x, 3.1f * Platfrom.transform.localScale.x);
        return new Vector3(randomX, randomY, randomZ);
    }
    public IEnumerator X2Scale()
    {
        multiplierIndicator.transform.localScale = new Vector3(0, 0, 0);
        for (float q = 0f; q < 1f; q += 0.1f)
        {
            multiplierIndicator.transform.localScale = new Vector3(q, q, q);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator ScoreScale()
    {
        score.transform.localScale = new Vector3(2, 2, 2);
        for (float q = 2f; q > 1f; q -= 0.1f)
        {
            score.transform.localScale = new Vector3(q, q, q);
            yield return new WaitForSeconds(0.01f);
        }
    }
}