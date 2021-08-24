using UnityEngine;
using UnityEngine.UI;

public class EndGameUiPopup : MonoBehaviour
{
    public Text LevelTxt;
    public Text PointsTxt;
    public GameObject[] ToDisable;
    public GameObject[] NetworkObjects;

    private void OnEnable()
    {
        SetUiState(false);
        CheckInternetConnection();
    }

   
    private void OnDisable()
    {
        SetUiState(true);
        SetObjectsState(true);
    }
    
    private void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SetObjectsState(Application.internetReachability == NetworkReachability.NotReachable);
        }
    }

    private void SetObjectsState(bool state)
    {
        foreach (var obj in NetworkObjects)
        {
            obj?.SetActive(state);
        }
    }

    private void SetUiState(bool state)
    {
        foreach (var obj in ToDisable)
        {
            obj.SetActive(state);
        }
    }

}
