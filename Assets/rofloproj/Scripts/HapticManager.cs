using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class HapticManager : MonoBehaviour
{
    public static HapticManager Instance;
    public bool HapticsEnabled;
    public GameObject On;
    public GameObject Off;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        HapticsEnabled = PlayerPrefs.GetInt("HapticsEnabled") == 0; // 0= enbled 1=disabled
        On.SetActive(!HapticsEnabled);
        On.SetActive(HapticsEnabled);
    }
    public void SetHapticBool()
    {
        HapticsEnabled = !HapticsEnabled;
        if (HapticsEnabled)
        {
            PlayerPrefs.SetInt("HapticsEnabled", 0);
        }
        else
        {
            PlayerPrefs.SetInt("HapticsEnabled", 1);
        }
        On.SetActive(!HapticsEnabled);
        On.SetActive(HapticsEnabled);
    }
    public void PlayPickuoHaptic()
    {
        if (HapticsEnabled)
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
    }

    public void PlayDeathHaptic()
    {
        if (HapticsEnabled)
        {
            MMVibrationManager.Haptic(HapticTypes.Failure);
        }
    }

    public void PlayWinHaptic()
    {
        if (HapticsEnabled)
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        }
    }

}
