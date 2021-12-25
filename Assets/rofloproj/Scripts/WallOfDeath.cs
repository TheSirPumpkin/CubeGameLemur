using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    private const int maxLevel = 10;

    public LookAtPlayer atPlayer;
    public float MoveSpeed;
    public PauseMenu PauseMenu;
    public float SpeedDecrease;


    private bool startLevel;
    private void OnEnable()
    {
        atPlayer = GameObject.FindObjectOfType<LookAtPlayer>();
        PauseMenu.ResumeGameEvent.AddListener(Run);
    }
    private void OnDisable()
    {
        PauseMenu.ResumeGameEvent.RemoveListener(Run);
    }
    private void Run()
    {
        StartCoroutine(StartChase());
    }
    IEnumerator StartChase()
    {
        yield return new WaitForSeconds(PlayerPrefs.GetInt("Level") / 2);
        startLevel = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startLevel)
        {
            float distance = Vector3.Distance(transform.position, atPlayer.PlayersMiddle);

            if (distance > 15)
            {
                if (PlayerPrefs.GetInt("Level") <= maxLevel)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed * PlayerPrefs.GetInt("Level") / 2);
                }
                else
                {
                    transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed * (maxLevel / 2));
                }

                return;
            }

            if (PlayerPrefs.GetInt("Level") <= maxLevel)
            {
                transform.Translate((Vector3.right * Time.deltaTime * MoveSpeed * PlayerPrefs.GetInt("Level") / 2) / SpeedDecrease);
            }
            else
            {
                transform.Translate((Vector3.right * Time.deltaTime * MoveSpeed * (maxLevel / 2)) / SpeedDecrease) ;
            }

        }
    }
}
