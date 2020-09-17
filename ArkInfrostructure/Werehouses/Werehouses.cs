using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Werehouses 
{
    
    //----Count----//
    private static int _metal = 0;
    private static int _yellowGem = 0;
    private static int _blueGem = 0;
    private static int _greenGem = 0;
    public static int Metal => _metal;
    public static int YellowGem => _yellowGem;
    public static int BlueGem => _blueGem;
    public static int GreenGem => _greenGem;

    //----Income----//
    private static int _metalIncome = 0;
    private static int _yellowGemIncome = 0;
    private static int _blueGemIncome = 0;
    private static int _greenGemIncome = 0;
    public static int MetalIncome => _metalIncome;
    public static int YellowGemIncome => _yellowGemIncome;
    public static int BlueGemIncome => _blueGemIncome;
    public static int GreenGemIncome => _greenGemIncome;

    public static void AddResours(ResoursesType type, int count)
    {
        switch (type)
        {
            case ResoursesType.Metal:
                {
                    _metal += count;
                }break;
            case ResoursesType.YellowGem:
                {
                    _yellowGem += count;
                }
                break;
            case ResoursesType.BlueGem:
                {
                    _blueGem += count;
                }
                break;
            case ResoursesType.GreenGem:
                {
                    _greenGem += count;
                }
                break;
        }
    }
    public static void SubResours(ResoursesType type, int count)
    {
        switch (type)
        {
            case ResoursesType.Metal:
                {
                    _metal -= count;
                }
                break;
            case ResoursesType.YellowGem:
                {
                    _yellowGem -= count;
                }
                break;
            case ResoursesType.BlueGem:
                {
                    _blueGem -= count;
                }
                break;
            case ResoursesType.GreenGem:
                {
                    _greenGem -= count;
                }
                break;
        }
    }
    public static void AddCost(Costs cost)
    {
        _metal += cost.metal;
        _yellowGem += cost.yellowGem;
        _blueGem += cost.blueGem;
        _greenGem += cost.greenGem;

    }
    public static bool SubCost(Costs cost)
    {
        if (_metal < cost.metal || _yellowGem < cost.yellowGem || _blueGem < cost.blueGem || _greenGem < cost.greenGem)
        {
            return false;
        }
        _metal -= cost.metal;
        _yellowGem -= cost.yellowGem;
        _blueGem -= cost.blueGem;
        _greenGem -= cost.greenGem;
        
        return true;
    }
    public static void CalculateIncome()
    {
        int MinerShips = AvailableShips.MinerShips;
        _metalIncome = MinerShips * 10;
        _yellowGemIncome = MinerShips * 2;
        _blueGemIncome = MinerShips * 2;
        _greenGemIncome = MinerShips * 2;
    }

    public static void AddIncome()
    {
        _metal += _metalIncome;
        _yellowGem += _yellowGemIncome;
        _blueGem += _blueGemIncome;
        _greenGem += _greenGemIncome;
    }
    
}

public enum ResoursesType
{
    Metal,
    YellowGem,
    BlueGem,
    GreenGem
}

