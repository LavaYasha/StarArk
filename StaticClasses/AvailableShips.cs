using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AvailableShips
{
    private static int _yellowShips = 0;
    private static int _blueShips = 0;
    private static int _greenShips = 0;
    private static int _minerShips = 0;

    public static int YellowShips => _yellowShips;
    public static int BlueShips => _blueShips;
    public static int GreenShips => _greenShips;
    public static int MinerShips => _minerShips;

    public static void AddShip(int count, string type)
    {
        switch (type)
        {
            case "Yellow":
                {
                    _yellowShips += count;
                }
                break;
            case "Blue":
                {
                    _blueShips += count;
                }
                break;
            case "Green":
                {
                    _greenShips += count;
                }
                break;
            case "Miner":
                {
                    _minerShips += count;
                }
                break;
        }
    }

    public static void SubShip(int count, string type)
    {
        switch (type)
        {
            case "Yellow":
                {
                    _yellowShips -= count;
                }
                break;
            case "Blue":
                {
                    _blueShips -= count;
                }
                break;
            case "Green":
                {
                    _greenShips -= count;
                }
                break;
            case "Miner":
                {
                    _minerShips -= count;
                }
                break;
        }
    }
}

