using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public Transform[] Bonus;
    //public Transform GroundLevel;
    // public Transform Platfrom;
    public ParticleSystem pointParticles;
    public float MagnetSpeed = 1;
    private bool startMagnet;

    private void OnEnable()
    {
        PlayerMovement.magnetEnabled += StartMagnet;
    }
    private void OnDisable()
    {
        PlayerMovement.magnetEnabled -= StartMagnet;
    }

    private void Start()
    {
        //Respawn();
    }
    void Update()
    {
        //if (transform.position.y <= GroundLevel.position.y)
        //{
        //    Respawn();
        //}
    }
    //public void Respawn()
    //{
    //    int chance = UnityEngine.Random.Range(0, 30);
    //    if (chance == 0)
    //    {
    //        SpawnBonus();
    //    }

    //    return;
    //}

    public void StartMagnet(Transform player)
    {
        CancelInvoke();
        startMagnet = true;
        StartCoroutine(MagnetToPlayer(player));
        Invoke("StopMagnet", PlayerPrefs.GetFloat("MagnetTime"));
    }
    private void StopMagnet()
    {
        startMagnet = false;
    }
    //private void SpawnBonus()
    //{
    //    Transform bonus = Instantiate(Bonus[UnityEngine.Random.Range(0, Bonus.Length)], GetRandomVector(), Quaternion.identity);
    //}
    //private Vector3 GetRandomVector()
    //{
    //    float randomX = UnityEngine.Random.Range(-.5f * Camera.main.transform.position.x, .5f * Camera.main.transform.position.x);
    //    float randomY = UnityEngine.Random.Range(10 * Platfrom.transform.localScale.x, 20 * Platfrom.transform.localScale.x);
    //    float randomZ = UnityEngine.Random.Range(-3.1f * Platfrom.transform.localScale.x, 3.1f * Platfrom.transform.localScale.x);
    //    return new Vector3(randomX, randomY, randomZ);
    //}

    private IEnumerator MagnetToPlayer(Transform player)
    {
        //  GetComponent<Rigidbody>().isKinematic = true;
        while (startMagnet)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, MagnetSpeed * (Time.deltaTime / distance) * player.transform.localScale.x);
            yield return null;
        }
        // GetComponent<Rigidbody>().isKinematic = false;
    }
    public void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag == "Player")
        {
           
            Instantiate(pointParticles, transform.position, Quaternion.identity);
            PointManager.Instance.AddScore(transform.position);
            //Respawn();
            gameObject.SetActive(false);
            
        }
        //if (collide.gameObject.tag == "Enemy")
        //{
        //    //Respawn();
        //}

        //if (collide.gameObject.tag == "Death")
        //{
        //    //Respawn();
        //}
    }
}