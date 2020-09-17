using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemySquad
{
    private static List<IWarship> _squad = new List<IWarship>();
    public static List<IWarship> Squad => _squad;

    public static void GatherASquad(List<IWarship> newSquad)
    {
        if (_squad.Count > 0)
        {
            _squad.Clear();
        }
        _squad.AddRange(newSquad);
    }

    public static void Clear()
    {
        _squad.Clear();
    }
}
