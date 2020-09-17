using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Tile : MonoBehaviour
{
    public UnityEvent change = null;

    public bool walkable = true;
    public bool selectable = false;
    public bool current = false;
    public bool target = false;
    public bool active = false;

    private List<Tile> _nearNeighbors = new List<Tile>();
    private List<Tile> _diagonalNeighbors = new List<Tile>();
    private List<Tile> _allNeighbors = new List<Tile>();
    public List<Tile> NearNeighbors => _nearNeighbors;
    public List<Tile> DiagonalNeighbors => _diagonalNeighbors;
    public List<Tile> AllNeghbors => _allNeighbors;

    //for find selectable tiles
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    //A* needed parametres
    public float f = 0;
    public float h = 0;
    public float g = 0;

    private Renderer _render;
    
    Color curType = new Color(0, 0, 1, 0.3f);
    Color actType = new Color(1, 0, 0, 0.3f);
    Color targType = new Color(1, 0, 1, 0.3f);
    Color selectType = new Color(0, 1, 0, 0.3f);
    Color defType = new Color(1, 1, 1, 0.075f);

    #region MonoBehaviour
    private void Start()
    {
        _render = gameObject.GetComponent<Renderer>();
        change = new UnityEvent();
        change.AddListener(delegate { this.SetColor(); });
        change.Invoke();
    }

    public void SetColor()
    {
        if (current)
        {
            _render.material.color = curType;
        }
        else if (active)
        {
            _render.material.color = actType;
        }
        else if (target)
        {
            _render.material.color = targType;
        }
        else if (selectable)
        {
            _render.material.color = selectType;
        }
        else
        {
            _render.material.color = defType;
        }

        isTileWalkable();
    }

    #endregion
    public bool isTileWalkable()
    {
        RaycastHit hit;
        bool tileOccupied = Physics.Raycast(gameObject.transform.position, Vector3.up, out hit, 1);
        if (tileOccupied)
        {
            walkable = false;
        }
        else
        {
            walkable = true;
        }
        
        return walkable;
    }

    public GameObject GetObjectOnTile()
    {
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position, Vector3.up, out hit, 1))
        {
            return hit.transform.gameObject;
        }
        return null;
    }
    public void Clear()
    {
        selectable = false;
        current = false;
        target = false;
        active = false;
        f = g = h = 0;
        change.Invoke();
    }

    public void tReset()
    {
        Clear();

        visited = false;
        parent = null;
        distance = 0;
    }

    public void AddNeighbors(Tile naighbor)
    {
        _nearNeighbors.Add(naighbor);
    }

    public void AddDiagonalNeighbors(Tile dn)
    {
        _diagonalNeighbors.Add(dn);
    }
    public void FindAllNeighbors()
    {
        AllNeghbors.AddRange(_nearNeighbors);
        AllNeghbors.AddRange(_diagonalNeighbors);
    }
}
