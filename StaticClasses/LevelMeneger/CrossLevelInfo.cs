using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossLevelInfo
{
    private static string _loadedScene = "";

    public static string LoadedScene => _loadedScene;

    public static void LoadScene(string sceneName)
    {
        _loadedScene = sceneName;
    }

    public static Costs MissionReward { private set; get; }

    public static void SetReward(Costs reward) { MissionReward = reward; }
    public static void SetReward(int metal, int yellowGem, int blueGem, int greenGem)
    {
        Costs temp = new Costs();

        temp.metal = metal;
        temp.yellowGem = yellowGem;
        temp.blueGem = blueGem;
        temp.greenGem = greenGem;

        SetReward(temp);
    }
}
