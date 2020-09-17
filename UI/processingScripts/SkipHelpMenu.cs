using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SkipHelpMenu : MonoBehaviour
{
    private void Start()
    {
        InGameCursor.CursorInit();
        Screen.SetResolution(1980, 1080, true);
    }
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
