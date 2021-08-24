using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Text DistanceText;
    public Text CubesText;
    public Slider DistanceSlider;
    public Slider WallDistanceSlider;
    public LookAtPlayer CameraControlls;
    private float distance;
    public Transform PlatformEater;

    void Start()
    {
        SetDistance();
        SetDistanceWall();
    }
    void Update()
    {
        DistanceSlider.value = Vector3.Distance(GameManager.Instance.EndPlatform.GetComponentInChildren<LevelEndDetector>().transform.position, CameraControlls.PlayersMiddle);
        WallDistanceSlider.value = Vector3.Distance(GameManager.Instance.EndPlatform.GetComponentInChildren<LevelEndDetector>().transform.position, PlatformEater.position);
        DistanceText.text = "" + (int)(DistanceSlider.value);
        CubesText.text = CameraControlls.PlayersNumber.ToString();
    }
    public void DisableBar()
    {
        DistanceSlider.gameObject.SetActive(false);
        WallDistanceSlider.gameObject.SetActive(false);
    }
    private void SetDistance()
    {
        distance = Vector3.Distance(GameManager.Instance.EndPlatform.GetComponentInChildren<LevelEndDetector>().transform.position, GameManager.Instance.JoystickMove.rb[0].transform.position);
        DistanceSlider.minValue = 0;
        DistanceSlider.maxValue = distance;
    }
    private void SetDistanceWall()
    {
        distance = Vector3.Distance(GameManager.Instance.EndPlatform.GetComponentInChildren<LevelEndDetector>().transform.position, PlatformEater.position);
        WallDistanceSlider.minValue = 0;
        WallDistanceSlider.maxValue = distance;
    }
}
