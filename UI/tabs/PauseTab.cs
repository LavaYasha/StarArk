using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseTab : Tab
{
    [SerializeField] Button _exitButton;
    [SerializeField] Button _backToArk;
    [SerializeField] Tab _confirmTab;

    private void Start()
    {
        _exitButton.onClick.AddListener(() => 
        {
            _confirmTab.Show("You realy want to exit?", () => 
            {
                Exit();
            });
        });
        _backToArk.onClick.AddListener(() => 
        {
            _confirmTab.Show("You realy want to back to Ark?\nAll successes will be closed.", () => 
            {
                BackToArk();
            });
        });
    }

    public override void Show()
    {
        base.Show();
        Time.timeScale = 0f;
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void BackToArk()
    {
        SceneManager.LoadScene("ArkBase");
    }
}
