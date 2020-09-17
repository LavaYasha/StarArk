using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArkMenu : MonoBehaviour
{
    public void StartBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
