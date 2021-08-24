using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBridgeTrigger : MonoBehaviour
{
    public GameObject Bridge;
    public GameObject HintUI;
    public GameObject Hint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>())
        {
            other.GetComponentInParent<PlayerMovement>().DetectDeath();
            HintUI.SetActive(false);
            Hint.SetActive(false);
            Bridge.SetActive(true);
            GetComponent<Collider>().enabled = false;
            GameManager.Instance.RespawnController.LastBridge = transform;
        }
    }
}
