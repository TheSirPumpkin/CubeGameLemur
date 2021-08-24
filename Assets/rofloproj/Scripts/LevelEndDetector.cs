using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndDetector : MonoBehaviour
{
    public float EndGameTimer;
    public Camera EndPointCam;
    public Transform EndPoint;
    public WallOfDeath WallOfDeath;
    public JoystickMove JoystickMove;
    public GameObject Firewroks;
    // private Collider firstCol;
    private bool levelSet;
    private bool loadLock;
    private bool levelEnded;
    private PointManager pointManager;
    private void Awake()
    {
        JoystickMove = GameObject.FindObjectOfType<JoystickMove>();
        WallOfDeath = GameObject.FindObjectOfType<WallOfDeath>();
    }
    private void Start()
    {
        pointManager = PointManager.Instance;
    }
    private void Update()
    {
        if (!loadLock && levelSet)
        {
            if (JoystickMove.rb.Count <= pointManager.ScoreMultiplierValue)
            {
                loadLock = true;
                Invoke("NextLevel", 1f);
            }
            if (pointManager.ScoreMultiplierValue == 10)
            {

                loadLock = true;
                Invoke("NextLevel", 1f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>() && !levelSet)
        {
            if (!levelSet)
            {
                GameManager.Instance.GetComponent<ProgressBarController>().DisableBar();
                Firewroks.SetActive(true);
                levelSet = true;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                PlayerPrefs.SetInt("LevelUi", PlayerPrefs.GetInt("LevelUi") + 1);
                foreach (var r in JoystickMove.rb)
                {
                    r.GetComponentInParent<PlayerMovement>().enabled = false;
                }
                StartCoroutine(JoystickMove.EndGameMove());
                this.WallOfDeath.gameObject.SetActive(false);
            }
        }
    }


    private void NextLevel()
    {
        GameManager.Instance.EnableNextLevelPopup();
    }
}
