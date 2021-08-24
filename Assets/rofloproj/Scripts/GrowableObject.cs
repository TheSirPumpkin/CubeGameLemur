using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowableObject : MonoBehaviour
{
    public static GrowableObject Instance;
    public Color CurrentColor;
    public GameObject ObstaclePrefab;
    public Transform TransfromToGrow;
    private Vector3 initScale;
    public bool locked;
    private void OnEnable()
    {
        initScale = TransfromToGrow.localScale;
        //PointManager.objectGrowDelegate += Grow;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //else
        //{
        //    Destroy(this);
        //}
        //DontDestroyOnLoad(this);
    }
    private void Update()
    {
       // if (!TransfromToGrow)
      //  {
        //    TransfromToGrow = GameObject.Find("Platforms").transform;
        //    initScale = TransfromToGrow.localScale;
       // }
    }
    private void Grow(int multiplier)
    {
      //  if (!locked)
     //  {
          
       //     this.StartCoroutine(GrowCoroutine(multiplier));
      //  }
    }

    private IEnumerator GrowCoroutine(int multiplier)
    {
        //ObstaclePart[] obstacles = GameObject.FindObjectsOfType<ObstaclePart>();
        //Debug.Log(TransfromToGrow.GetComponentInChildren<Animation>().gameObject.name);
        //TransfromToGrow.GetComponentInChildren<Animation>().Play("LevelGrow2");
        ////TransfromToGrow.GetComponentInChildren<Outline>().enabled = true;
        //locked = true;
        //float endScale = initScale.x + (multiplier / 10f);
        //while (TransfromToGrow.localScale.x < endScale)
        //{
        //    TransfromToGrow.localScale += new Vector3(multiplier / 10f, multiplier / 10f, multiplier / 10f) * Time.deltaTime;
        //    if (obstacles.Length != 0)
        //    {
        //        foreach (var obs in obstacles)
        //        {
        //            obs.transform.position += new Vector3(0, 0.01f, 0);
        //            obs.transform.localScale += new Vector3(0.003f, 0.003f, 0.003f);
        //        }
        //    }
        //    if (TransfromToGrow.localScale.x >= endScale) break;
        //    yield return null;
        //}
        //TransfromToGrow.localScale = new Vector3(endScale, endScale, endScale);
        //initScale = TransfromToGrow.localScale;
        //SpawnObstacle();
        //locked = false;
        yield return null;
    }
    private void SpawnObstacle()
    {
        //for (int i = 0; i < PointManager.Instance.CollectedPoints / 2; i++)
        //{
        //    float randomX = Random.Range(-2.5f * initScale.x, 2.5f * initScale.x);
        //    float randomZ = Random.Range(-2.5f * initScale.x, 2.5f * initScale.x);
        //    ObstacleRoot obst= Instantiate(ObstaclePrefab, new Vector3(randomX, initScale.y + ObstaclePrefab.transform.localScale.y * 0.9f, randomZ), Quaternion.identity).GetComponent<ObstacleRoot>();
        //    foreach (var obstacle in obst.GetComponent<ObstacleRoot>().Obstacles)
        //    {
        //        if (obstacle.GetComponent<MeshRenderer>())
        //        {
        //            foreach (var mat in obstacle.GetComponent<MeshRenderer>().materials)
        //            {
        //                mat.color = CurrentColor;
        //            }
        //        }
        //    }
        //}

     
    }
    //private void OnDisable()
    //{
    //    PointManager.playerGrowDelegate -= Grow;
    //}
    //private void OnDestroy()
    //{
    //    PointManager.playerGrowDelegate -= Grow;
    //}
}
