
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    [SerializeField] TeamFormation _teamForm;
    [SerializeField] GameObject _playerSquadParent;
    [SerializeField] GameObject _enemySquadParent;
    [Header("Info Panels")]
    [SerializeField] GameObject _infoPanelToggle;
    [SerializeField] GameObject _contentFieldForPlayerInfo;
    [SerializeField] GameObject _contentFieldForEnemyInfo;
    [SerializeField] GameObject _infoPanelTemplate;
    private bool showInfoPanel = false;
    private List<IWarship> _playerSquad;
    private List<TackticMove> _playerSquadGO = new List<TackticMove>();
    private List<TackticMove> _enemySquadGO = new List<TackticMove>();
    private bool PlayModeOn = false;
    public void Play()
    {
        if (!PlayModeOn)
        {

            Init();
        }
    }
    private void ToggleInfoPanels()
    {
        if (showInfoPanel)
        {
            _infoPanelToggle.SetActive(true);

        }
        else
        {
            _infoPanelToggle.SetActive(false);
        }
    }

    private void AddShipInfoPonel(GameObject ship, bool playerTeam)
    {
        GameObject go = Instantiate(_infoPanelTemplate, playerTeam ? _contentFieldForPlayerInfo.transform : _contentFieldForEnemyInfo.transform);
        var infoRender = go.GetComponent<ShipInfoRenderer>();
        infoRender.SetCharacter(ship.GetComponent<CharInfo>());
    }

    private List<IWarship> DebugEnemySquadCreated()
    {
        List<IWarship> squad = new List<IWarship>();

        for(int i = 0; i < _playerSquad.Count + 1; i++)
        {
            squad.Add(_playerSquad[Random.Range(0, _playerSquad.Count)]);
        }

        return squad;
    }

    private void Init()
    {
        
        showInfoPanel = true;
        gameObject.SetActive(false);
        ToggleInfoPanels();

        PlayModeOn = true;
        PlayerGroup.GatherAGroup(_teamForm.PlayerSquad);
        _playerSquad = _teamForm.PlayerSquad;
        //=================== DEBUG =====================//
        EnemySquad.GatherASquad(DebugEnemySquadCreated());
        //===============================================//
        int i = 0;
        foreach (var unit in _playerSquad)
        {
            AvailableShips.SubShip(1, unit.WeaponType);
            i++;
            var freeTile = GetFreeTile().gameObject;

            if(freeTile == null)
            {
                return; //not enough space
            }

            var ship = Instantiate(unit.Ship, _playerSquadParent.transform);
            
            float halfShipHeight = ship.GetComponent<Collider>().bounds.extents.y;
            float halfTileHeight = freeTile.GetComponent<Collider>().bounds.extents.y;

            Vector3 freeCell = freeTile.transform.position;
            freeCell.y += halfTileHeight + halfShipHeight;

            ship.name = unit.Name + " Player " + i;
            ship.tag = "PlayerUnit";
            ship.transform.position = freeCell;
            ship.AddComponent<PlayerMove>();
            ship.AddComponent<CharInfo>();

            ship.GetComponent<CharInfo>().Init(unit);
            _playerSquadGO.Add(ship.GetComponent<PlayerMove>());
            AddShipInfoPonel(ship, true);
            ship.GetComponent<PlayerMove>().PreInit();
        }
        TurnManager.AddTeam(_playerSquadGO);
        i = 0;
        foreach (var unit in EnemySquad.Squad)
        {
            i++;
            var freeTile = GetFreeTileForEnemy().gameObject;

            if (freeTile == null)
            {
                return; //not enough space
            }

            var ship = Instantiate(unit.Ship, _enemySquadParent.transform);

            float halfShipHeight = ship.GetComponent<Collider>().bounds.extents.y;
            float halfTileHeight = freeTile.GetComponent<Collider>().bounds.extents.y;

            Vector3 freeCell = freeTile.transform.position;
            freeCell.y += halfTileHeight + halfShipHeight;

            ship.name = unit.Name + " Enemy " + i;
            ship.tag = "EnemyUnit";
            ship.transform.position = freeCell;
            ship.AddComponent<NPCmove>();
            ship.AddComponent<CharInfo>();

            ship.GetComponent<CharInfo>().Init(unit);
            _enemySquadGO.Add(ship.GetComponent<NPCmove>());
            AddShipInfoPonel(ship, false);
            ship.GetComponent<NPCmove>().PreInit();
        }
        TurnManager.AddTeam(_enemySquadGO);
    }
    private Tile GetFreeTile()
    {
        MapGenerator mg = GameObject.FindGameObjectWithTag("TileMap").GetComponent<MapGenerator>();

        Map map = mg.CreatedMap;
        var tilemap = mg.TileMap;
        int i = 0;
        while (true)
        {
            int RandomRow = System.Convert.ToInt32(Random.value * System.Convert.ToSingle(map.MaxRawForPlayer + 1));
            int RandomCol = System.Convert.ToInt32(Random.value * System.Convert.ToSingle(map.MaxColumnForPlayer + 1));

            if (RandomRow > map.MaxRawForPlayer)
            {
                RandomRow = map.MaxRawForPlayer;
            }
            else if (RandomRow < 0)
            {
                RandomRow = 0;
            }
            if (RandomCol > map.MaxColumnForPlayer)
            {
                RandomCol = map.MaxColumnForPlayer;
            }
            else if (RandomCol < 0)
            {
                RandomCol = 0;
            }

            if (tilemap[RandomCol][RandomRow].walkable)
            {
                tilemap[RandomCol][RandomRow].walkable = false;
                return tilemap[RandomCol][RandomRow];
            }
            i++;
            if (i > map.MaxColumnForPlayer + map.MaxRawForPlayer)
            {
                break;
            }
           
        }
        return null;
    }
    private Tile GetFreeTileForEnemy()
    {
        MapGenerator mg = GameObject.FindGameObjectWithTag("TileMap").GetComponent<MapGenerator>();

        Map map = mg.CreatedMap;
        var tilemap = mg.TileMap;
        int i = 0;
        while (true)
        {
            int RandomRow = System.Convert.ToInt32(Random.value * System.Convert.ToSingle(map.Height - map.MaxRawForPlayer) + map.MaxRawForPlayer + 4);
            int RandomCol = System.Convert.ToInt32(Random.value * System.Convert.ToSingle(map.Width - map.MaxColumnForPlayer) + map.MaxColumnForPlayer + 4);

            if (RandomRow > map.Height - 1)
            {
                RandomRow = map.Height - 1;
            }
            else if (RandomRow < map.MaxRawForPlayer + 4)
            {
                RandomRow = map.MaxRawForPlayer + 4;
            }
            if (RandomCol > map.Width - 1)
            {
                RandomCol = map.Width - 1;
            }
            else if (RandomCol < map.MaxColumnForPlayer + 4)
            {
                RandomCol = map.MaxColumnForPlayer + 4;
            }

            if (tilemap[RandomCol][RandomRow].walkable)
            {
                tilemap[RandomCol][RandomRow].walkable = false;
                return tilemap[RandomCol][RandomRow];
            }
            i++;
            if (i > map.MaxColumnForPlayer + map.MaxRawForPlayer)
            {
                break;
            }

        }
        return null;
    }
}
