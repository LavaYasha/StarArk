using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Tab
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
    public void SetGlobalVolume(float curValue)
    {
        AudioListener.volume = curValue;
    }

    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(5);
    }
}
