using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBattle : MonoBehaviour
{
    [SerializeField] private EndBattleTab endGameTab;
    private static EndBattle instance;
    
    private void Start()
    {
        instance = this;
    }
    public void CheckTeamsCount()
    {
        foreach (var item in TurnManager.Teams) {
            if (item.Value.Count <= 0)
            {
                if(item.Key == "PlayerUnit")
                {
                    Lose();
                }
                else
                {
                    Win();
                }
            }
        }
    }
    public static void CheckTeamsCount_static()
    {
        instance.CheckTeamsCount();
    }
    private void Win()
    {
        Debug.Log("Win");
        endGameTab.ShowResult(true);
        endGameTab.Show(() =>
        {
            Werehouses.AddCost(CrossLevelInfo.MissionReward);
            Time.timeScale = 1f;
            SceneManager.LoadScene("ArkBase");
        });
    }

    private void Lose()
    {
        Debug.Log("Lose");
        endGameTab.ShowResult(false);
        endGameTab.Show(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("ArkBase");
        });
    }
}
