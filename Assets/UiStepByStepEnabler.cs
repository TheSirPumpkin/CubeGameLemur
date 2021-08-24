using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStepByStepEnabler : MonoBehaviour
{
    public GameObject[] UiElements;


    private void OnEnable()
    {
        for (int i = 0; i < UiElements.Length; i++)
        {
            UiElements[i].SetActive(true);
            //yield return new WaitForEndOfFrame();
        }
        //StartCoroutine(EnableUi());
    }

    private IEnumerator EnableUi()
    {
        for (int i = 0; i < UiElements.Length; i++)
        {
            UiElements[i].SetActive(true);
            yield return null;
            //yield return new WaitForEndOfFrame();
        }

    }
}
