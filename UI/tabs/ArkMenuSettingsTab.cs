using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArkMenuSettingsTab : Tab
{
    [SerializeField] GameObject UIblocker;
    [SerializeField] ConfirmTab confirm;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
    public override void Show()
    {
        base.Show();

        UIblocker.SetActive(true);
    }
    public override void Hide()
    {
        base.Hide();

        UIblocker.SetActive(false);
    }
    public void BackToMainMenu()
    {
        confirm.Show("You are realy want to back to main menu?", () =>
         {
             SceneManager.LoadScene("MainMenu");
         }, () =>
         {
             confirm.Hide();
         });
    }

    public void Save()
    {
        PersistentData temp = new PersistentData();
        temp.yellowShips = AvailableShips.YellowShips;
        temp.blueShips = AvailableShips.BlueShips;
        temp.greenShips = AvailableShips.GreenShips;
        temp.metal = Werehouses.Metal;
        temp.yellowGem = Werehouses.YellowGem;
        temp.blueGem = Werehouses.BlueGem;
        temp.greenGem = Werehouses.GreenGem;
        string json = JsonUtility.ToJson(temp);
        File.WriteAllText(Application.persistentDataPath + "/Save.gm", json);
        Debug.Log(Application.persistentDataPath + "/Save.gm");
    }

    public void Exit()
    {
        confirm.Show("You are realy want to Exit?", () =>
        {
            Application.Quit();
        }, () =>
        {
            confirm.Hide();
        });
    }
}

public class PersistentData
{
    public int yellowShips;
    public int blueShips;
    public int greenShips;
    public int minerShips;
    public int metal;
    public int yellowGem;
    public int blueGem;
    public int greenGem;
}