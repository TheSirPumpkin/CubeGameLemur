using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePart : MonoBehaviour
{
    public GameObject Particles;
    public void DestroyOnGrow()
    {
        Particles.SetActive(true);
        Particles.transform.parent = null;
        transform.root.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().StoneBreaker)
            {
                Particles.SetActive(true);
                Particles.transform.parent = null;
                transform.root.gameObject.SetActive(false);
            }
        }
    }
}
