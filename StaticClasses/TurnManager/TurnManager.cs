using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TurnManager : MonoBehaviour
{
    public static Dictionary<string, List<TackticMove>> Teams = new Dictionary<string, List<TackticMove>>();
    static Queue<string> TurnKey = new Queue<string>();
    static Queue<TackticMove> turnTeam = new Queue<TackticMove>();

    static bool TeamTurnEnded = true;

    private void Update()
    {
        if (TeamTurnEnded)
        {
            InitTearnTeamQueue();
        }
    }

    private static void InitTearnTeamQueue()
    {
        
        if (Teams.Count > 0)
        {
            //Debug.Log($"Team Name = {TurnKey.Peek()}");
            List<TackticMove> TeamList = Teams[TurnKey.Peek()];

            foreach (TackticMove unit in TeamList)
            {
                turnTeam.Enqueue(unit);
            }

            StartTurn();
        }
    }

    private static void StartTurn()
    {
        
        TeamTurnEnded = false;
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        TackticMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = TurnKey.Dequeue();
            TurnKey.Enqueue(team);
            //InitTearnTeamQueue();
            var tilemap = GameObject.FindGameObjectWithTag("TileMap").transform;
            foreach (Transform item in tilemap)
            {
                item.GetComponent<Tile>().tReset();
            }
            TeamTurnEnded = true;
        }
        EndBattle.CheckTeamsCount_static();
    }

    public static void UnitDeath(GameObject unit)
    {
        var tm = unit.GetComponent<TackticMove>();
        Teams[unit.tag].Remove(tm);
    }

    public static GameObject GetCurrentPlayerTurn()
    {
        if (turnTeam.Count > 0)
        {
            return turnTeam.Peek().gameObject;
        }
        else
        {
            return null;
        }
    }
    public static void AddTeam(List<TackticMove> team)
    {
        if (!Teams.ContainsKey(team[0].gameObject.tag))
        {
            Teams.Add(team[0].gameObject.tag, team);
            TurnKey.Enqueue(team[0].gameObject.tag);
        }
    }
}
