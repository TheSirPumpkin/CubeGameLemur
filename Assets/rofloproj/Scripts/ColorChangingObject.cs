using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingObject : MonoBehaviour
{
    Color lerpedColor = Color.red;
    MeshRenderer mesh;
    private void OnEnable()
    {
        PointManager.changeColor += ChangeColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        var col = GrowableObject.Instance.CurrentColor;
        foreach (var mat in mesh.materials)
        {
            mat.color = col;
        }
    }

    void Update()
    {
        //lerpedColor = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        //foreach (var mat in mesh.materials)
        //{
        //    mat.color = lerpedColor;
        //}
    }

    public void ChangeColor()
    {
        try
        {
            StartCoroutine(ChangeColorCoroutine());
        }
        catch (System.Exception e)
        {
        }
    }

    private IEnumerator ChangeColorCoroutine()
    {
        float timeElapsed = 0f;
        float totalTime = 2f;

        while (timeElapsed < totalTime)
        {
            lerpedColor = Color.Lerp(mesh.materials[0].color, GrowableObject.Instance.CurrentColor, timeElapsed / totalTime);
            timeElapsed += Time.deltaTime;
            foreach (var mat in mesh.materials)
            {
                mat.color = lerpedColor;
            }
            yield return null;
        }
    }
    private void OnDisable()
    {
        Destroy(this);
    }
}
