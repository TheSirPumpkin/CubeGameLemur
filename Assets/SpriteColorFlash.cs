using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorFlash : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float delay = 0.3f;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        while (true)
        {
            sprite.color = new Color(200f, 200f, 200f, 0.5f);
            yield return new WaitForSeconds(delay);
            Color color = Color.red;
            color.a = 0.5f;
            sprite.color = color;
            
            yield return new WaitForSeconds(delay);
        }
    }

}
