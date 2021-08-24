using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHit : MonoBehaviour
{
    private float radius = 1.5F;
    private float power = 100f;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HammerHit");
        float force = 70f;

        Vector3 dir = other.transform.position - transform.position;
        dir.x = 0;
        dir.y = 0;
        dir.z *= 10;
        dir = dir.normalized;

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponentInParent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 1.0F);
               
            }
        }
        other.GetComponentInParent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);


    }
}
