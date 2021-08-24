using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private bool levelCoin;
    [SerializeField]
    private MeshRenderer Mesh;
    [SerializeField]
    private float deathTime;
    private float initDeathTime;
   
    void Start()
    {
        initDeathTime = deathTime;
    }
 
    IEnumerator Flick()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            deathTime -= Time.deltaTime;
            if (deathTime <= initDeathTime / 5f)
            {
                yield return new WaitForSeconds(0.1f);
                Mesh.enabled = false;
                yield return new WaitForSeconds(0.1f);
                Mesh.enabled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (!levelCoin)
            {
                Destroy(gameObject, deathTime);
                StartCoroutine(Flick());
            }
        }
    }
}
