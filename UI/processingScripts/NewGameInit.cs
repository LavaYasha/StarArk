using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameInit : MonoBehaviour
{
    [Header("Start Resourses")]
    [SerializeField] int _metal;
    [SerializeField] int _yellowGem;
    [SerializeField] int _blueGem;
    [SerializeField] int _greenGem;
    [Header("Start Ships")]
    [SerializeField] int _yellowShips;
    [SerializeField] int _bleuShips;
    [SerializeField] int _greenShips;
    [SerializeField] int _minerShips;

    public void InitializeNewGame()
    {
        //---<resourses init>---//

        //---<resourses init>---//

        //---<Ship init>---//
        AvailableShips.AddShip(_yellowShips, "Yellow");
        AvailableShips.AddShip(_bleuShips, "Blue");
        AvailableShips.AddShip(_greenShips, "Green");
        AvailableShips.AddShip(_minerShips, "Miner");
        //---<Ship init>---//
    }
}
