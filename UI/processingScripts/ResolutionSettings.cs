using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResolutionSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolution;

    public void SetResolution()
    {
        switch (resolution.value)
        {
            case 0:
                Screen.SetResolution(1980, 1080, true);
                break;
            case 1:
                Screen.SetResolution(1280, 720, true);
                break;
            case 2:
                Screen.SetResolution(1024, 576, true);
                break;
        }
    }
}
