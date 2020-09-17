using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCmove : TackticMove
{
    private GameObject PathLine;
    private CharInfo InfoCurShip;
    private string _weaponType;
    private string _shieldType;
    public void Start()
    {
        Init();
        PathLine = Resources.Load("Prefabs/Path") as GameObject;
        InfoCurShip = gameObject.GetComponent<CharInfo>();
        _weaponType = InfoCurShip.WeaponType;
        _shieldType = InfoCurShip.ShieldType;
    }
    private void OnMouseEnter()
    {
        GameObject currentPlayer = TurnManager.GetCurrentPlayerTurn();
        CharInfo info = currentPlayer.GetComponent<CharInfo>(); 
        _info.Choosen = true;
        if (GetDistanceToTarget(currentPlayer) <= info.ShootingRange && currentPlayer.tag == "PlayerUnit")
        {
            InGameCursor.SetTargetCursor();
        }
    }
    private void OnMouseExit()
    {
        _info.Choosen = false;
        InGameCursor.SetDefaultCursor();
    }

    private void OnDestroy()
    {
        _info.Choosen = false;
        InGameCursor.SetDefaultCursor();
    }
    public void Update()
    {
        if (!turn)
        {
            return;
        }
        
        if (!moving && !shooting)
        {
            GameObject bestTarget = FindBestTurget();
            //FindSelectableTiles();

            Debug.Log("Target = " + bestTarget.name + "\nDistance to target" + GetDistanceToTarget(bestTarget) + "\nis visible = " + IsTargetVisible(bestTarget));


            if (GetDistanceToTarget(bestTarget) > shootingRange || !IsTargetVisible(bestTarget))
            {
                CalculatePath(bestTarget);
            }
            else
            {
                moving = false;
                shooting = true;
            }
        }
        else if (shooting && !moving)
        {
            if (!holding)
            {
                GameObject bestTarget = FindBestTurget();
                moving = false;
                tackticalShooting.Fire(bestTarget);
                holding = true;
            }
            else
            {
                holdingCount++;
                if (holdingCount >= 50)
                {
                    holdingCount = 0;
                    tackticalShooting.EndFire();
                    shooting = false;
                    holding = false;
                }
            }
        }
        else if (moving && !shooting)
        {
            Move();
        }
    }

    /// <summary>
    /// NPC Intelect
    /// </summary>
    /// 
    /// 1st: low HP (1-2 shots) and not so far (one max distance and attac on next turn)
    ///     then chuse this ship
    /// 2nd: opposite type
    /// 3rd: choice nearest ship
    private GameObject FindBestTurget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("PlayerUnit");
        GameObject targetOpposite = null;
        GameObject targetNearest = null;

        float minHPopposite = Mathf.Infinity;
        float minSPopposite = Mathf.Infinity; 

        float minHPnearest = Mathf.Infinity;
        float minSPnearest = Mathf.Infinity;

        float minDistanceOpposite = Mathf.Infinity;
        float minDistance = Mathf.Infinity;

        foreach (var t in targets)
        {
            GetDistanceToTarget(t);
            var info = t.GetComponent<CharInfo>();
            if ((info.HitPoints <= 10 && info.ShieldPoints <= 10) || (info.HitPoints <= 20 && info.ShieldPoints <= 0))
            {
                return t;
            }
            else if (_weaponType != info.ShieldType &&
                    ((_weaponType == "Yellow" && info.ShieldType == "Blue") || 
                    (_weaponType == "Blue" && info.ShieldType == "Green")   ||
                    (_weaponType == "Green" && info.ShieldType == "Yellow")))
            {
                 if(minHPopposite > info.HitPoints && GetDistanceToTarget(t) <= minDistanceOpposite)
                 {
                     minDistanceOpposite = GetDistanceToTarget(t);
                     minHPopposite = info.HitPoints;
                     targetOpposite = t;
                 }
            }
            if (GetDistanceToTarget(t) < minDistance)
            {
                minDistance = GetDistanceToTarget(t);
                targetNearest = t;
            }
        }

        if (targetOpposite == targetNearest)
        {
            return targetOpposite;
        }
        if (minHPopposite - minHPnearest < 10)
        {
            return targetOpposite;
        }

        return targetNearest;
    }

    private void CalculatePath(GameObject target)
    {
        Tile targetTile = GetTargetTile(target);
        FindPathToTarget(targetTile);
    }
}