using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tab : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private TextMeshProUGUI _discription;
    [SerializeField] private Text _defaultDisctription; 
    [Space]
    [SerializeField] public bool _isOpen = false;
    private void Start()
    {
        Hide();
    }
    public virtual void Show()
    {
        _isOpen = true;
        gameObject.SetActive(true);
    }

    public virtual void Show(Vector3 positon)
    {
        Show();
        gameObject.transform.position = positon;
    }
    public virtual void Show(Vector3 positon, params int[] args)
    {
        Show();
        gameObject.transform.position = positon;
    }
    public virtual void Show(Action onOk, Action onCancel)
    {
        _okButton.onClick.AddListener(() => { onOk(); });
        _cancelButton.onClick.AddListener(() => { onCancel(); });
        Show();
    }

    public virtual void Show(string discription, Action onOk, Action onCancel)
    {
        if (_discription != null)
        {
            _discription.text = discription;
        }
        else
        {
            _defaultDisctription.text = discription;
        }
        _okButton.onClick.AddListener(() => { onOk(); });
        _cancelButton.onClick.AddListener(() => { onCancel(); });
        Show();
    }

    public virtual void Show(Action onOk)
    {
        _okButton.onClick.AddListener(() => { onOk(); });
        Show();
    }
    public virtual void Show(string discription, Action onOk)
    {
        if (_discription != null)
        {
            _discription.text = discription;
        }
        else
        {
            _defaultDisctription.text = discription;
        }
        _okButton.onClick.AddListener(() => { onOk(); });
        _cancelButton.onClick.AddListener(() => { Hide(); });
        Show();
    }

    public virtual void Hide()
    {
        _isOpen = false;
        gameObject.SetActive(false);
    }

    public virtual void Toggle()
    {
        _isOpen = !_isOpen;
        if (_isOpen)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
