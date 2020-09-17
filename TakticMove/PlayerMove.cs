using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TacticalShooting))]
public class PlayerMove : TackticMove
{
    private GameObject PathLine;
    private GameObject fireTarget;
    public void Start()
    {
        Init();
        PathLine = Resources.Load("Prefabs/Path") as GameObject;
    }
    private void OnMouseEnter()
    {
        _info.Choosen = true;
    }
    private void OnMouseExit()
    {
        _info.Choosen = false;
    }
    public void Update()
    {
        if (!turn)
        {
            DeletePathLine();
            return;
        }
        
        if (!moving && !shooting)
        {
            FindSelectableTiles();
            LevelOutGO();
            CheckMouse();
        }
        else if (shooting && !moving)
        {
            if (!holding)
            {
                moving = false;
                tackticalShooting.Fire(fireTarget);
                holding = true;
            }
            else
            {
                holdingCount++;
                if (holdingCount >= 50)
                {
                    holdingCount = 0;
                    fireTarget = null;
                    tackticalShooting.EndFire();
                    shooting = false;
                    holding = false;
                }
            }
        }
        else
        {
            Move();
        }
    }
    private void LevelOutGO()
    {
        float halfHeight = gameObject.GetComponent<Collider>().bounds.extents.y;
        Vector3 pos = gameObject.transform.position;
        pos.y = halfHeight + currentTile.GetComponent<Collider>().bounds.extents.y;
        gameObject.transform.position = pos;
    }
    private void CheckMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, Input.mousePosition, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "EnemyUnit")
            {
                var temptarget = hit.transform.gameObject;
                if (Input.GetMouseButtonUp(1) && 
                    GetDistanceToTarget(temptarget) <= shootingRange &&
                    IsTargetVisible(temptarget))
                {
                    fireTarget = temptarget;
                    shooting = true;

                    RemoveSelectedTiles();
                    ClearSelectableTiles();
                    DeletePathLine();
                }

            }
            if (hit.collider.tag == "Tile")
            {
                Tile t = hit.collider.GetComponent<Tile>();

                if (t.selectable) 
                {
                    if (Input.GetMouseButtonUp(1))
                    {
                        PrepareToMove(t);
                    }

                    DeletePathLine();
                    DrawPath(t);

                    t.active = true;
                    t.change.Invoke();
                }
                else
                {
                    ClearSelectableTiles();
                    DeletePathLine();
                }
            }
            else
            {
                ClearSelectableTiles();
                DeletePathLine();
            }
        }
        else
        {
            ClearSelectableTiles();
            DeletePathLine();
        }
    }

    private void DrawPath(Tile t)
    {
        ClearSelectableTiles();
        Stack<Tile> _path = createPath(t);
        int i = 0;

        GameObject go = Instantiate(PathLine);
        LineRenderer _pathLine = go.GetComponent<LineRenderer>();

        go.transform.SetParent(gameObject.transform);
        _pathLine.positionCount = _path.Count;

        foreach (Tile ptile in _path)
        {
            _pathLine.SetPosition(i, ptile.transform.position);
            i++;
        }
        i = 0;
    }

    private void ClearSelectableTiles()
    {
        var tmap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<MapGenerator>().TileMap;

        foreach (var item in tmap)
        {
            foreach (Tile tile in item)
            {
                if (tile.selectable)
                {
                    tile.active = false;
                    tile.change.Invoke();
                }
            }
        }
    }

    private void DeletePathLine()
    {
        foreach (Transform item in gameObject.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
