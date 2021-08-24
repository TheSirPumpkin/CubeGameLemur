using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3 posAdjust;// (0,12,0)
    public JoystickMove Players;
    public float SmoothTime = 0.3f;
    public Transform EndGameTarget = null;
    public Vector3 PlayersMiddle;
    public int PlayersNumber;
    public ScoreMultiplier[] EndGameTargets;
    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    


    private void Start()
    {
        cam = Camera.main;
        PlayerPrefs.SetInt("CleaeredLevels", 1);
        EndGameTargets = GameObject.FindObjectsOfType<ScoreMultiplier>();
    }
    private void Update()
    {
        //EndGameTarget = FindObjectOfType<ScoreMultiplier>().transform;
        // StartCoroutine(AdjustHeight());
    }
    private void FixedUpdate()
    {
        SetCameraPos();
    }
    public void SetEndGameTarget(int index)
    {
        foreach (var target in EndGameTargets)
        {
            if (target.Multiply == index)
            {
                EndGameTarget = target.transform;
            }
        }
    }
    void SetCameraPos()
    {
        Vector3 middle = Vector3.zero;
        int numPlayers = 0;

        for (int i = 0; i < Players.rb.Count; ++i)
        {
            if (Players.rb[i] == null || !Players.rb[i].gameObject.activeSelf)
            {
                continue; //skip, since player is deleted
            }
            middle += Players.rb[i].transform.position;
            numPlayers++;
        }//end for every player

        //take average:
        if (numPlayers != 0)
        {
            middle /= numPlayers;
        }

        else if (EndGameTarget == null)
        {
            return;
        }
        if (EndGameTarget == null)
        {
            cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(middle.x + posAdjust.x, middle.y + posAdjust.y, middle.z + posAdjust.z), Time.deltaTime * 10);
        }
        else
        {
            cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(EndGameTarget.transform.position.x+ posAdjust.x, EndGameTarget.transform.position.y + posAdjust.y+5, EndGameTarget.transform.position.z + posAdjust.z-5), Time.deltaTime * 2);
        }
        PlayersMiddle = middle;
        PlayersNumber = numPlayers;
    }



}