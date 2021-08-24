using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    public GameObject BombExplosion;
    public Transform Platfrom;
    public Transform Target;
    private Rigidbody rigidbody;
    private bool targetHit;
    private GameObject BombVFX;
    private bool isRunning;
    private void Start()
    {
        Destroy(GetComponent<MeshRenderer>());
       
        BombVFX = Instantiate(BombExplosion);
        BombVFX.SetActive(false);
        rigidbody = GetComponent<Rigidbody>();
        Platfrom = transform.parent;
        StartCoroutine(Respawn());
    }

    void Update()
    {
        if (transform.position.y <= 1.5f)
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            isRunning = false;
            StartCoroutine(Respawn());
        }
    }
    public IEnumerator Respawn()
    {
        BombVFX.transform.localPosition = transform.position;
        BombVFX.SetActive(true);
        rigidbody.isKinematic = true;

        yield return new WaitForSeconds(0.5f);

        if (isRunning)
        {
            yield return null;
        }
        isRunning = true;
        Shoot();
    }

    private void Shoot()
    {
        StopAllCoroutines();
        GetComponentInChildren<MeshRenderer>().enabled = true;
        rigidbody.isKinematic = false;
        Vector3 forwardForce = Vector3.forward * UnityEngine.Random.Range(-4f, -6f);
        Vector3 upForce = Vector3.up * UnityEngine.Random.Range(7f, 12f);
        targetHit = false;
        Target.parent = transform;
        Target.position = transform.position;
        transform.parent = Platfrom;
        transform.localPosition = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(forwardForce, ForceMode.Impulse);
        rigidbody.AddForce(upForce, ForceMode.Impulse);
        transform.parent = null;
        StartCoroutine(PlotTrajectory(Platfrom.position, forwardForce + upForce, Time.deltaTime*5, Time.deltaTime * 100));
    }

    public void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.tag == "Ground")
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            isRunning = false;
            StartCoroutine(Respawn());
        }

        if (ground.gameObject.tag == "Player")
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            isRunning = false;
            StartCoroutine(Respawn());
        }
    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public IEnumerator PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        Vector3 prev = start;
        float t = timestep;
        for (int i = 1; ; i++)
        {
            t = timestep * i;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            if (Physics.Linecast(prev, pos))
            {
                Target.parent = null;
                Target.position = pos;
                Target.position = new Vector3(Target.position.x, 1.6f, Target.position.z);
                targetHit = true;
                break;
            }
            prev = pos;
            yield return new WaitForFixedUpdate();
        }

    }

}