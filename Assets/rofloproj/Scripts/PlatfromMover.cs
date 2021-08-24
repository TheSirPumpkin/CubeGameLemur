using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromMover : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Patterns;
    [SerializeField]
    private PatternPart[] PatternParts;
    private void Start()
    {
        Respawn();
    }
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime*2);
    }

    public void Respawn()
    {
        foreach(var pattern in Patterns)
        {
            pattern.SetActive(false);
        }
       
        Patterns[Random.Range(0, Patterns.Length)].SetActive(true);
    }
}
