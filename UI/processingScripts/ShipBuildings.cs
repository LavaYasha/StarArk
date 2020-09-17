using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShipBuildings : MonoBehaviour
{
    struct ShipTime
    {
        public Costs cost;
        public string type;
        public int time;
    }
    [SerializeField] private List<GameObject> slots;
    private Dictionary<GameObject, ShipTime> CostructShips = new Dictionary<GameObject, ShipTime>();
    ShipTime emptySlot = new ShipTime();
    private void Start()
    {
        
        emptySlot.time = 0;
        emptySlot.cost = new Costs();
        emptySlot.type = null;
        for (int i = 0; i < slots.Count; i++)
        {
            CostructShips.Add(slots[i], emptySlot);
        }
    }

    public void AddShipToConstruct(string type, Costs cost)
    {
        ShipTime busySlot = new ShipTime();
        busySlot.cost = cost;
        if (type != "Miner")
        {
            busySlot.time = 5;
        }
        else
        {
            busySlot.time = 10;
        }
        busySlot.type = type;
        int i = 0;
        foreach(var item in CostructShips)
        {
            ShipTime temp = item.Value;
            if(temp.type == null)
            {
                CostructShips[item.Key] = busySlot;
                break;
            }
            i++;
        }
        if (i < slots.Count)
        {
            if (Werehouses.SubCost(cost))
            {
                slots[i].GetComponent<ShipBuildingsRenderer>().RenderShip(type, busySlot.time);
            }
        }
    }

    public void RemoveShipFromConstruct(GameObject client)
    {
        int i = 0;
        foreach (var item in CostructShips)
        {
            if(item.Key == client)
            {
                Werehouses.AddCost(item.Value.cost);
                CostructShips[item.Key] = emptySlot;
                break;
            }
            i++;
        }
        slots[i].GetComponent<ShipBuildingsRenderer>().RenderShip(emptySlot.type, 0);
    }
    public void ProcessBuilding()
    {
        int i = 0;
        foreach(var item in CostructShips.Keys.ToList())
        {
            ShipTime temp = CostructShips[item];
            if (temp.type != null)
            {
                temp.time -= 1;
                if (temp.time <= 0)
                {
                    FinishBuildingShip(item, temp.type);
                    temp.type = null;
                    temp.time = 0;
                }
                CostructShips[item] = temp;
                slots[i].GetComponent<ShipBuildingsRenderer>().RenderShip(CostructShips[item].type, CostructShips[item].time);
            }
            i++;
        }
    }
    private void FinishBuildingShip(GameObject client, string type)
    {
        RemoveShipFromConstruct(client);
        AvailableShips.AddShip(1, type);
    }
}
