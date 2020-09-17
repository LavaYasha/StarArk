using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class TeamFormation : MonoBehaviour
{
    [SerializeField] Sprite LockCell;
    [SerializeField] Warship[] UnitsType;
    [SerializeField] GameObject UnitButtonTemplate;
    [SerializeField] GameObject ContentScrollView;
    [SerializeField] GameObject CommandStructure;
    [SerializeField] GameObject _playButton;

    [SerializeField] int MaxSquadSize;
    
    private List<IWarship> _playerGroup =  new List<IWarship>();
    private List<IWarship> _enemyGroup = new List<IWarship>();

    private Map currentMap;

    private Dictionary<IWarship, int> ArrAvailableShips = new Dictionary<IWarship, int>();

    public List<IWarship> PlayerSquad  => _playerGroup;

    private void OnValidate()
    {
        if (MaxSquadSize > 10)
        {
            MaxSquadSize = 10;
        }
        else if (MaxSquadSize < 0)
        {
            MaxSquadSize = 0;
        }
    }
    private void Start()
    {
        //===============================//
        for (int i = 0; i < 6; i++)
        {
            AvailableShips.AddShip(1, UnitsType[Random.Range(0, 3)].WeaponType);
        }
        //===============================//

        AddAvailableShip(UnitsType[0], AvailableShips.YellowShips);
        AddAvailableShip(UnitsType[1], AvailableShips.BlueShips);
        AddAvailableShip(UnitsType[2], AvailableShips.GreenShips);

        Init();
    }

    private void Update()
    {
        if (PlayerSquad.Count < 1)
        {
            _playButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            _playButton.GetComponent<Button>().interactable = true;
        }
    }
    public void Init()
    {
        RenderAvailableShips();
        SetAvailableSquadCells();
    }
    public void AddToGroup(GameObject ReleasedButton)
    {
        if (_playerGroup.Count < MaxSquadSize)
        {
            var FU = ReleasedButton.GetComponentInChildren<FormationUnits>();
            IWarship AddedShip = FU.Ship;
            _playerGroup.Add(AddedShip);

            FU.ChangeCount(FU.Count - 1);
            SubAvailableShip(FU.Ship, 1);

            if (FU.Count <= 0)
            {
                ReleasedButton.SetActive(false);
            }
            var freeCell = GetFreeAvailableSquadCells();
            freeCell.AddUnit(AddedShip);
        }
        else
        {
            //TODO throw exception;
        }
    }

    public void RemoveInGroup(GameObject ReleasedButton)
    {
        var cell = ReleasedButton.GetComponent<SquadCell>();
        if (cell.Filled)
        {
            int index = -1;
            // определение ячейки
            for (int i = 0; i < CommandStructure.transform.OfType<Transform>().ToList().Count; i++)
            {
                var t = CommandStructure.transform.GetChild(i);
                if (t.name == ReleasedButton.name)
                {
                    index = i;
                }
            }

            if (index != -1)
            {
                IWarship removedShip;
                _playerGroup.RemoveAt(index);
                
                cell.RemoveUnit(out removedShip);
                MoveCells(index);
                BackToPrevState(removedShip);
            }
        }
        else
        {
            // TODO throw exception;
        }
    }
    
    private void BackToPrevState(IWarship removedSip)
    {
        var childs = ContentScrollView.transform.OfType<Transform>().ToList();
        foreach(var cell in childs)
        {
            var FU = cell.GetComponent<FormationUnits>();
            if (FU.Ship == removedSip)
            {
                FU.gameObject.SetActive(true);
                FU.ChangeCount(FU.Count + 1);
            }
        }
    }
    private void MoveCells(int lastIndex)
    {
        var childs = CommandStructure.transform.OfType<Transform>().ToList();
        for (int i = lastIndex; i < MaxSquadSize; i++)
        {
            if (i < MaxSquadSize - 1)
            {
                childs[i].GetComponent<SquadCell>().AddUnit(childs[i + 1].GetComponent<SquadCell>().Ship);
                childs[i + 1].GetComponent<SquadCell>().RemoveUnit();
            }
        }
    }
    private void RenderAvailableShips()
    {        
        foreach (var unit in ArrAvailableShips)
        {
            var go = Instantiate(UnitButtonTemplate, ContentScrollView.transform);
            var FU = go.GetComponent<FormationUnits>();
            var CustomES = go.GetComponent<CustomMouseEventSystem>();
            FU.SetUnit(unit.Key, unit.Value);
            FU.Render();
            go.name = unit.Key.Name;
            CustomES.RightMouseButtonReleased = null;
            CustomES.MiddleMouseButtonReleased = null;
            CustomES.LeftMouseButtonReleased.AddListener(delegate { AddToGroup(go); });
        }
    }
    private void SetAvailableSquadCells()
    {
        for (int i = 0; i < CommandStructure.transform.OfType<Transform>().ToList().Count; i++)
        {
            var t = CommandStructure.transform.GetChild(i);
            if (i >= MaxSquadSize)
            {
                t.GetComponent<SquadCell>().LockCell();
            }
        }
    }

    private SquadCell GetFreeAvailableSquadCells()
    {
        for (int i = 0; i < CommandStructure.transform.OfType<Transform>().ToList().Count; i++)
        {
            var t = CommandStructure.transform.GetChild(i).GetComponent<SquadCell>();
            if (t.Available && !t.Filled)
            {
                return t;
            }
        }
        return null;
    }

    private void AddAvailableShip(IWarship ship, int count)
    {
        if (ArrAvailableShips.ContainsKey(ship))
        {
            ArrAvailableShips[ship] += count;
        }
        else
        {
            ArrAvailableShips.Add(ship, count);
        }
    }

    private void SubAvailableShip(IWarship ship, int count)
    {
        if (ArrAvailableShips.ContainsKey(ship))
        {
            if (ArrAvailableShips[ship] - count > 0)
            {
                ArrAvailableShips[ship] -= count;
            }
            else
            {
                ArrAvailableShips.Remove(ship);
            }
        }
        else
        {
            //TODO: throw exeption
        }
    }

    private void SavePlayerGroup()
    {
        PlayerGroup.GatherAGroup(_playerGroup);
    }
}
