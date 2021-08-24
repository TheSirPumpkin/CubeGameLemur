using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPart : MonoBehaviour
{
    private bool playerOnTop;
    private float stayTimer=2f;
    private void FixedUpdate()
    {
       if(playerOnTop)
        {
            stayTimer -= Time.deltaTime;
            if(stayTimer<0)
            transform.localPosition -= new Vector3(0, .05f, 0) * Time.deltaTime;
        }
    }
    public void SetToDefault()
    {
        stayTimer = 2f;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer==9)
        {
            playerOnTop = true;
           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            playerOnTop = false;
        }
    }
}
