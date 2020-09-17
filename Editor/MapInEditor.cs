using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public static class MapInEditor 
{
    [MenuItem("Tool/Generate Map")]
    private static void GenerateMap()
    {
        MapGenerator mc = GameObject.FindGameObjectWithTag("TileMap").GetComponent<MapGenerator>();
        mc.GenerateOnStart = false;
        mc.Init();
    }

    [MenuItem("Tool/Clear Map")]
    private static void ClearMap()
    {
        MapGenerator mc = GameObject.FindGameObjectWithTag("TileMap").GetComponent<MapGenerator>();
        mc.GenerateOnStart = true;
        mc.Clear();
    }
}
