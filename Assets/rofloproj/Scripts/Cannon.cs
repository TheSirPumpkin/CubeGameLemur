using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject BombExplosion;
    public Animation Anim;
    public Material Hint;
    public Transform Bomb;
    public float FlyTimer;
    public float FlySpeed;
    public float ShotDelay;
    public float LoadSpeed;
    private GameObject BombVFX;
    private void Start()
    {
        BombVFX = Instantiate(BombExplosion);
        BombVFX.SetActive(false);
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        Color color = Hint.color;
        
        BombVFX.SetActive(false);
     
        float timer = FlyTimer;
        while (color.a < 1)
        {
            color.a += LoadSpeed * Time.deltaTime;
            Hint.color = color;
            yield return null;
        }
      
        Anim.Play();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Bomb.transform.Translate(-transform.forward * Time.deltaTime * FlySpeed);
            yield return null;
        }
        color.a = 0;
        Hint.color = color;
        BombVFX.transform.position = Bomb.transform.position;
        BombVFX.SetActive(true);
        Bomb.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(ShotDelay);
        StartCoroutine(Shoot());
    }
}
