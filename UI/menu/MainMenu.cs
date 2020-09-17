using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] NewGameInit _newGame;
    [SerializeField] Button _continueGame;
    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.gm"))
        {
            _continueGame.interactable = true;
        }
        else
        {
            _continueGame.interactable = false;
        }
    }
    public void NewGame()
    {
        _newGame.InitializeNewGame();
        SceneManager.LoadScene("ArkBase");
    }

    public void Resume()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/Save.gm");
        var go = JsonUtility.FromJson<PersistentData>(json);
        AvailableShips.AddShip(go.yellowShips, "Yellow");
        AvailableShips.AddShip(go.blueShips, "Blue");
        AvailableShips.AddShip(go.greenShips, "Green");
        AvailableShips.AddShip(go.minerShips, "Miner");
        Werehouses.AddResours(ResoursesType.Metal, go.metal);
        Werehouses.AddResours(ResoursesType.YellowGem, go.yellowGem);
        Werehouses.AddResours(ResoursesType.BlueGem, go.blueGem);
        Werehouses.AddResours(ResoursesType.GreenGem, go.greenGem);
        SceneManager.LoadScene("ArkBase");
    }
    public void Options()
    {
        optionsPanel.GetComponent<OptionsMenu>().Toggle();
    }

    public void AppExit()
    {
        Application.Quit();
    }

    IEnumerator Rofl(string scene)
    {
        yield return new WaitForSeconds(3.1f);
    }
}
