using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionInfoRender : MonoBehaviour
{
    [SerializeField] Button _startMission;
    [SerializeField] GameObject _uIblocker;
    [Header("Ships Type")]
    [SerializeField] TextMeshProUGUI _yellowType;
    [SerializeField] TextMeshProUGUI _blueType;
    [SerializeField] TextMeshProUGUI _greenType;
    [Header("Resourses")]
    [SerializeField] TextMeshProUGUI _metalText;
    [SerializeField] TextMeshProUGUI _yellowGemText;
    [SerializeField] TextMeshProUGUI _blueGemText;
    [SerializeField] TextMeshProUGUI _greenGemText;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
    public void Show(Action start, int metal = 0, int yellowGem = 0, int blueGem = 0, int greenGem = 0, int yellowType = 0, int blueType = 0, int greenType = 0)
    {
        gameObject.SetActive(true);
        _uIblocker.SetActive(true);

        _metalText.text = metal.ToString();
        _yellowGemText.text = yellowGem.ToString();
        _blueGemText.text = blueGem.ToString();
        _greenGemText.text = greenGem.ToString();

        _yellowType.text = yellowType.ToString();
        _blueType.text = blueType.ToString();
        _greenType.text = greenType.ToString();

        _startMission.onClick.AddListener(() => { start(); });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _uIblocker.SetActive(false);
    }
}
