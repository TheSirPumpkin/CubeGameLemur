using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    [SerializeField]
    private PlatformPart[] platformParts;
    [SerializeField]
    private PlatfromMover mover;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            transform.parent.localPosition = new Vector3(44.2f,transform.parent.position.y,transform.parent.position.z);
            foreach(var part in platformParts)
            {
                part.transform.localPosition = new Vector3(part.transform.localPosition.x,-0.1f, part.transform.localPosition.z);
                part.SetToDefault();
            }
            mover.Respawn();
        }
    }
}
