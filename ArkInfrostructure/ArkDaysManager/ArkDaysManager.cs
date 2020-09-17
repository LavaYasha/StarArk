using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArkDaysManager : MonoBehaviour
{
    [SerializeField] Button _endDay;
    [SerializeField] GameObject MissionTempalte;
    [SerializeField] GameObject MissionContentObject;
    [SerializeField] DaysRenderer _dayRenderer;
    [SerializeField] ShipBuildings _shipBuildings;
    private int _daysCount;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _daysCount = 0;
        _endDay.onClick.AddListener(() => { EndDay(); });
    }

    private void EndDay()
    {
        _daysCount++;
        _dayRenderer.RenderCurrentDay(_daysCount);
        ReplenishmentOfResourses();
        AddNewMisson();
        BuildShip();
    }

    private void ReplenishmentOfResourses()
    {
        Werehouses.AddIncome();
    }

    private void AddNewMisson()
    {
        //Such random to genarate mission
        if ((_daysCount % 5 == 0 && Random.Range(0, 100) < 50) ||
            (_daysCount % 2 == 0 && Random.Range(0, 100) < 10))
        {
            var mission = Instantiate(MissionTempalte, MissionContentObject.transform);
            mission.GetComponent<MissionInfo>().Init(Random.Range(1, 5));
        }
    }

    private void BuildShip()
    {
        _shipBuildings.ProcessBuilding();
    }
}
