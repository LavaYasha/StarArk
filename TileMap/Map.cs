using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="TileMap/CreateMap")]
public class Map : ScriptableObject
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private GameObject tile;
    [SerializeField] private int maxRawForPlayer;
    [SerializeField] private int maxColumnForPlayer;
    [SerializeField] private int minRowForEnemy;
    [SerializeField] private int minColomnForEnemy;
    public int Width => width;
    public int Height => height;
    public GameObject Tile => tile;

    public int MaxRawForPlayer => maxRawForPlayer; 
    public int MaxColumnForPlayer => maxColumnForPlayer; 
    public int MinRowForEnemy => minRowForEnemy; 
    public int MinColomnForEnemy => minColomnForEnemy; 
}
