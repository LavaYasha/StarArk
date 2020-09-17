using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Map map;
    [SerializeField] private List<List<Tile>> _tileMap = new List<List<Tile>>();

    
    public bool GenerateOnStart = false;

    public Map CreatedMap => map;
    public List<List<Tile>> TileMap => _tileMap; 

    private void Start()
    {
        if (GenerateOnStart)
        {
            Init();
        }
        //Debug.Log("ingame tilemap = " + _tileMap.Count);
    }

    public void Init()
    {
        Generate();
        SetNeighbors();
        SetDiagonalNeighbors();
        InitAllNeighbors();
    }

    public void Clear()
    {
        foreach(Transform go in gameObject.transform.Cast<Transform>().ToList())
        {
#if UNITY_EDITOR
            DestroyImmediate(go.gameObject);
#else
            Destroy(go.gameObject);
#endif
        }
        _tileMap.Clear();
    }
    private void Generate()
    {
        
        for(int i = 0; i < map.Width; i++)
        {
            _tileMap.Add(new List<Tile>());
            for (int j = 0; j < map.Height; j++)
            {
                var go = Instantiate(map.Tile) as GameObject;
                go.AddComponent<Tile>();
                _tileMap[i].Add(go.GetComponent<Tile>());
                go.transform.position = new Vector3(0, 0, 0);
                go.transform.SetParent(gameObject.transform);
                go.transform.position += new Vector3(i - map.Width / 2, 0, j - map.Height / 2);
                go.name = $"{i}-{j}";
            }
        }
    }

    private void SetNeighbors()
    {
        int Column = _tileMap.Count;
        for (int i = 0; i < Column; i++)
        {
            int Row = _tileMap[i].Count;
            for (int j = 0; j < Row; j++)
            {

                if (i > 0)
                {
                    //   *  
                    // N C * 
                    //   *
                    _tileMap[i][j].AddNeighbors(_tileMap[i - 1][j]);
                }
                if (j > 0)
                {
                    //   *  
                    // * C * 
                    //   N
                    _tileMap[i][j].AddNeighbors(_tileMap[i][j - 1]);
                }
                if (i < _tileMap.Count - 1)
                {
                    //   *  
                    // * C N 
                    //   *
                    _tileMap[i][j].AddNeighbors(_tileMap[i + 1][j]);
                }
                if (j < _tileMap[i].Count - 1)
                {
                    //   N  
                    // * C * 
                    //   *
                    _tileMap[i][j].AddNeighbors(_tileMap[i][j + 1]);
                }
            }
        }
    }

    private void SetDiagonalNeighbors()
    {
        int Column = _tileMap.Count;
        for (int i = 0; i < Column; i++)
        {
            int Row = _tileMap[i].Count;
            for (int j = 0; j < Row; j++)
            {

                if (i > 0 && j > 0)
                {
                    // * * *
                    // * C * 
                    // N * *
                    _tileMap[i][j].AddDiagonalNeighbors(_tileMap[i - 1][j - 1]);
                }
                if (i < _tileMap.Count - 1 && j > 0)
                {
                    // * * *
                    // * C * 
                    // * * N
                    _tileMap[i][j].AddDiagonalNeighbors(_tileMap[i + 1][j - 1]);
                    
                }
                if (i < _tileMap.Count - 1 && j < _tileMap[i].Count - 1)
                {
                    // * * N
                    // * C * 
                    // * * *
                    _tileMap[i][j].AddDiagonalNeighbors(_tileMap[i + 1][j + 1]);
                }
                if (i > 0 && j < _tileMap[i].Count - 1)
                {
                    // N * *
                    // * C * 
                    // * * *
                    _tileMap[i][j].AddDiagonalNeighbors(_tileMap[i - 1][j + 1]);
                }
            }
        }
    }

    private void InitAllNeighbors()
    {
        foreach(var item in TileMap)
        {
            foreach(var tile in item)
            {
                tile.FindAllNeighbors();
            }
        }
    }
}
