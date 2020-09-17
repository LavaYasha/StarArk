using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerGroup 
{
    public static int _maxShipsCount;
    
    private static List<IWarship> _group = new List<IWarship>();
    public static List<IWarship> Group => _group;

    public static void GatherAGroup(List<IWarship> newGroup)
    {
        _group.AddRange(newGroup);
    }

    public static void Clear()
    {
        _group.Clear();
    }
}
