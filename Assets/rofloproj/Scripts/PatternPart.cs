using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPart : MonoBehaviour
{
    private Vector3 defaultPos;
    [SerializeField]
    private Transform myPlatfrom;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, myPlatfrom.position.y+ myPlatfrom.localScale.y*2.4f, transform.position.z);
    }
  
}
