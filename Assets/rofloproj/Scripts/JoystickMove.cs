using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    private const int maxLevel = 10;
    public List<Rigidbody> rb = new List<Rigidbody>();
    public Joystick joystick;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float moveForce;
    private bool endGame;

    private void OnEnable()
    {
        joystick.Horizontal = joystick.Vertical = 0;
        horizontalMove = joystick.Horizontal = 0;
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("Level") <= maxLevel)
        {
            moveForce = 3f * (1.2f + (PlayerPrefs.GetInt("Level") / maxLevel));
        }
        else
        {
            moveForce = 3f * (2.2f);
        }
    }

    public void ResetMove()
    {
        horizontalMove = 0;
        verticalMove = 0;
    }
    public IEnumerator EndGameMove()
    {
        endGame = true;
        while (rb.Count > 0)
        {
            if (PointManager.Instance.ScoreMultiplierValue >= 10)
            {
                foreach (var rbody in rb)
                {
                    yield return new WaitForSeconds(0.5f);
                    rbody.isKinematic = true;
                }
                rb.Clear();
                StopAllCoroutines();
            }
            horizontalMove = moveForce * 2f;
            verticalMove = 0;
            foreach (var rbody in rb)
            {
                if (rbody.gameObject.activeSelf)
                {
                    rbody.velocity = new Vector3(horizontalMove, rbody.velocity.y, verticalMove);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    private void JoystickMovement()
    {
        horizontalMove = joystick.Horizontal * moveForce;//joystick.Vertical * moveForce;
        verticalMove = joystick.Vertical * moveForce;//joystick.Horizontal * -moveForce;
        foreach (var rbody in rb)
        {
            if (rbody.gameObject.activeSelf)
            {
                rbody.velocity = new Vector3(horizontalMove, rbody.velocity.y, verticalMove);
            }
        }

    }
    private void FixedUpdate()
    {
        if (endGame)
        {
            return;
        }
        JoystickMovement();
    }
    private void OnCollisionStay(Collision collision)
    {
        //JoystickMovement();
    }
}