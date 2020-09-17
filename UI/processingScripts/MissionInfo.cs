using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MissionInfo : MonoBehaviour
{
    [Header("Ship Types")]
    [SerializeField] private int _yellowType;
    [SerializeField] private int _blueType;
    [SerializeField] private int _greenType;
    [Header("Resourses")]
    [SerializeField] private int _metalResourses;
    [SerializeField] private int _yellowGemResourses;
    [SerializeField] private int _blueGemResourses;
    [SerializeField] private int _greenGemResourses;
    [Header("Mission info interface")]
    [SerializeField] private TextMeshProUGUI Discriptions;
    [SerializeField] private MissionInfoRender _infoUI;
    [SerializeField] private WarshipTypes _types;
    private int bonusFactor = 4;
    private void Start()
    {
        Init(4);
    }
    public void Init(int shipsCount)
    {
        Discriptions.text = GetRandomDiscriptions();
        _types = FindObjectOfType<WarshipTypes>();
        _infoUI = FindObjectOfType<MissionInfoRender>(true);
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => { _infoUI.Show(() => { StartMission(); },_metalResourses, _yellowGemResourses, _blueGemResourses, _greenGemResourses, _yellowType, _blueType, _greenType); });
        RandomiseEnemySquad(shipsCount);
        RandomiseMissionResourse(shipsCount);
    }
    public void StartMission()
    {
        List<IWarship> enemySquad = new List<IWarship>();
        for (int i = 0; i < _yellowType; i++)
        {
            enemySquad.Add(_types.ShipTypes[0]);
        }
        for (int i = 0; i < _blueType; i++)
        {
            enemySquad.Add(_types.ShipTypes[1]);
        }
        for (int i = 0; i < _greenType; i++)
        {
            enemySquad.Add(_types.ShipTypes[2]);
        }
        CrossLevelInfo.SetReward(_metalResourses, _yellowGemResourses, _blueGemResourses, _greenGemResourses);
        EnemySquad.GatherASquad(enemySquad);
        SceneManager.LoadScene("Battle");
    }
    private void RandomiseEnemySquad(int maxShip)
    {
        _yellowType = 0;
        _blueType = 0;
        _greenType = 0;
        while (maxShip > 0)
        {
            int type = Random.Range(0, 3);
            switch (type)
            {
                case 0:
                    {
                        int r = Random.Range(0, maxShip + 1);
                        _yellowType += r;
                        maxShip -= r;
                    }
                    break;
                case 1:
                    {
                        int r = Random.Range(0, maxShip + 1);
                        _blueType += r;
                        maxShip -= r;
                    }
                    break;
                case 2:
                    {
                        int r = Random.Range(0, maxShip + 1);
                        _greenType += r;
                        maxShip -= r;
                    }
                    break;
            }
        }
    }
    private void RandomiseMissionResourse(int maxShip)
    {
        int maxGem = Random.Range(0, maxShip * bonusFactor);
        int maxMetal = Random.Range(10, (maxShip * 10) * (bonusFactor / 2));

        _metalResourses = Random.Range(10, maxMetal + 1);
        _yellowGemResourses = Random.Range(0, maxGem + 1);
        _blueGemResourses = Random.Range(0, maxGem + 1);
        _greenGemResourses = Random.Range(0, maxGem + 1);
    }
    private string GetRandomDiscriptions()
    {
        string result = "Discriptions:\n";
        List<string> randomDisc = new List<string>();
        randomDisc.Add("Enemy attact our Ark!\nJust kill them all!");
        randomDisc.Add("We find pirates base.\nClear the sector.");
        randomDisc.Add("Our miners are at risk.\nProtect them");
        randomDisc.Add("Peace ship came under fire");
        randomDisc.Add("Enemy spoted!");
        result += randomDisc[Random.Range(0, randomDisc.Count)];
        return result;
    }
}
