using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    public Rigidbody rb;
    public float accelForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        AccelControl();
    }
    public void AccelControl()
    {
        Vector3 acc = Input.acceleration;
        rb.AddForce(acc.x * accelForce, 0, acc.y * accelForce);
        /*
        rb.AddTorque(acc.y * accelForce, 0, acc.x * accelForce);
        rb.AddForce(acc.x * 100 * Time.deltaTime, 0, acc.y * 100 * Time.deltaTime);
        */
    }
}