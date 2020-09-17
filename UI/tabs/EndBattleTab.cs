using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndBattleTab : Tab
{
    [SerializeField] Image backGround;
    [SerializeField] TextMeshProUGUI Lable;
    public void ShowResult(bool isPlayerWin)
    {
        Time.timeScale = 0f;
        if (isPlayerWin)
        {
            backGround.color = Color.green;
            Lable.text = "WIN!";
        }
        else
        {
            backGround.color = Color.red;
            Lable.text = "LOSE!";
        }
    }
}
