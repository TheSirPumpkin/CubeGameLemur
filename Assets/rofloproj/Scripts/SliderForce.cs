using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderForce : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.GetComponentInParent<PlayerMovement>())
        //{
        //    Debug.Log("Slide");
        //    collision.transform.localPosition += new Vector3((transform.rotation.z / 90) * 300f,0, 0f);
        //}
    }
}
