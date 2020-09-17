using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TacticalShooting))]
public class TackticMove : MonoBehaviour
{
    [SerializeField] protected bool turn = false;

    [SerializeField] protected bool moving = false;
    [SerializeField] protected int maxDistance;
    [SerializeField] protected float moveSpeed = 3.0f;
    [SerializeField] protected Tile currentTile = null;
    [SerializeField] protected List<Tile> selectableTiles = new List<Tile>();
    [SerializeField] protected float shootingRange;
    [SerializeField] protected bool shooting = false;
    [SerializeField] protected TacticalShooting tackticalShooting;
    [SerializeField] protected bool holding = false;
    [SerializeField] protected int holdingCount = 0;
    protected CharInfo _info;
    private bool prepareShooting = false;
    private Cinemachine.CinemachineVirtualCamera gamecamera;
    private Stack<Tile> path = new Stack<Tile>();
    private float halfHeight;
    private Vector3 heading = new Vector3();
    private Vector3 velocity = new Vector3();
    #region MonoBehaviour
    
    private void OnValidate()
    {
        if(maxDistance < 0)
        {
            maxDistance = 0;
        }
        else if (maxDistance > 30)
        {
            maxDistance = 30;
        }
    }
    #endregion
    #region Public
    public void PreInit()
    {
        tackticalShooting = GetComponent<TacticalShooting>();

        _info = gameObject.GetComponent<CharInfo>();
        _info.SetName(gameObject.name);
    }
    public void BeginTurn()
    {
        gamecamera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        gamecamera.Follow = gameObject.transform;
        turn = true;    
        Init();
    }

    public void EndTurn()
    {
        turn = false;
    }
    public Stack<Tile> createPath(Tile end)
    {
        path.Clear();
        Tile next = end;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
        return path;
    }
    #endregion
    #region Protected
    protected void Init()
    {

        _info = GetComponent<CharInfo>();
        shootingRange = _info.ShootingRange;
        maxDistance = _info.MoveDistance;
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    protected void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
        currentTile.change.Invoke();
    }

    protected Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile targetTile = null;
        if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            targetTile = hit.collider.GetComponent<Tile>();
        }
        return targetTile;
    }

    protected void FindSelectableTiles()
    {
        RemoveSelectedTiles();
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();
        
        process.Enqueue(currentTile);
        currentTile.visited = true;

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;
            t.change.Invoke();
            if (t.distance < maxDistance && t.NearNeighbors.Count > 0)
            {
                foreach (Tile tile in t.NearNeighbors)
                {
                    if (!tile.visited && tile.walkable)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
                if (t.distance < maxDistance - 1)
                {
                    foreach (Tile tile in t.DiagonalNeighbors)
                    {
                        if (!tile.visited && tile.walkable)
                        {
                            tile.parent = t;
                            tile.visited = true;
                            tile.distance = 2 + t.distance;
                            process.Enqueue(tile);
                        }
                    }
                }
            }
        }
    }
    

    protected void FindPathToTarget(Tile targetTile)
    {
        RemoveSelectedTiles();
        GetCurrentTile();

        List<Tile> OpenTiles = new List<Tile>();
        List<Tile> ClosedTiles = new List<Tile>();

        OpenTiles.Add(currentTile);

        currentTile.h = Vector3.Distance(currentTile.transform.position, targetTile.transform.position);
        currentTile.f = currentTile.g + currentTile.h;

        while (OpenTiles.Count > 0)
        {
            Tile cell = GetLowerFTile(OpenTiles);
           
            //cell.target = true;

            ClosedTiles.Add(cell);
            

            if (cell == targetTile)
            {
                Tile endtile = GetEndTile(targetTile);
                PrepareToMove(endtile);
                return;
            }

            foreach(var tile in cell.AllNeghbors)
            {
                if (ClosedTiles.Contains(tile))
                {
                    continue;
                }
                else if (OpenTiles.Contains(tile))
                {
                    float NewG = cell.g + Vector3.Distance(tile.transform.position, cell.transform.position);

                    if (NewG < tile.g)
                    {
                        tile.parent = cell;

                        tile.g = NewG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    if (!tile.walkable && tile != targetTile)
                    {
                        ClosedTiles.Add(tile);
                        continue;
                    }

                    tile.parent = cell;

                    tile.g = cell.g + Vector3.Distance(tile.transform.position, cell.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, targetTile.transform.position);
                    tile.f = tile.g + tile.h;

                    OpenTiles.Add(tile);
                }
            }
        }
        return;
    }

    protected bool IsTargetInRage(Tile tile, Tile targetTile)
    {
        if (Vector3.Distance(tile.transform.position, targetTile.transform.position) <= shootingRange)
        {
            return true;
        }
        return false;
    }

    protected bool IsTargetVisible(GameObject target)
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, (target.transform.position - gameObject.transform.position), out hit))
        {
            if(hit.transform.gameObject != target)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    protected void PrepareToMove(Tile end)
    {
        end.Clear();
        end.target = true;
        end.change.Invoke();
        currentTile.walkable = true;
        createPath(end);
        moving = true;
    }

    protected void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;
            target.y = halfHeight + t.GetComponent<Collider>().bounds.extents.y;
            if (Vector3.Distance(gameObject.transform.position, target) >= 0.1f)
            {
                CalculateHeading(target);
                SetHorizontalSpeed();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                gameObject.transform.position = target;
                path.Pop();
            }
        }
        else if (prepareShooting)
        {
            RemoveSelectedTiles();
            moving = false;
            shooting = true;
        }
        else
        {
            RemoveSelectedTiles();
            moving = false;
            TurnManager.EndTurn();
        }
    }
    protected float GetDistanceToTarget(GameObject target)
    {
        //Debug.Log($"{gameObject.name} to {target.name} distance = {Vector3.Distance(gameObject.transform.position, target.transform.position)}");
        return Vector3.Distance(gameObject.transform.position, target.transform.position);
    }
    protected void RemoveSelectedTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile.change.Invoke();
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.tReset();
        }

        selectableTiles.Clear();
    }
    #endregion
    #region Private
    private Tile GetLowerFTile(List<Tile> open)
    {
        Tile lowest = open[0];

        foreach (var tile in open)
        {
            if (lowest.f > tile.f)
            {
                lowest = tile;
            }
        }

        open.Remove(lowest);
        return lowest;
    }

    private Tile GetEndTile(Tile tile)
    {
        Stack<Tile> TempPath = new Stack<Tile>();

        Tile next = tile.parent;
        while (next != null)
        {
            TempPath.Push(next);
            next = next.parent;
        }

        Tile EndTile = null;
        for (int i = 0; i < maxDistance; i++)
        {
            if (EndTile != null && EndTile.DiagonalNeighbors.Contains(TempPath.Peek()))
            {
                if (i < maxDistance - 1)
                {
                    i++;
                }
            }
            EndTile = TempPath.Pop();

            if (IsTargetInRage(EndTile, tile) && IsTargetVisible(tile.GetObjectOnTile()))
            {
                prepareShooting = true;
                break;
            }
        }
        EndTile.walkable = false;
        return EndTile;
    }
    

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalSpeed()
    {
        velocity = heading * moveSpeed;
    }
    #endregion
}
