using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationUnits : MonoBehaviour
{
    [SerializeField] Image _iconField;
    [SerializeField] Text _textField;

    private IWarship _ship;
    private int _count;

    public IWarship Ship => _ship;

    public int Count => _count;

    public void SetUnit(IWarship ship, int count)
    {
        _ship = ship;
        _count = count;
    }
    public void Render()
    {
        _iconField.sprite = _ship.Icon;
        _textField.text = _count.ToString();
    }
    public void ChangeCount(int newCount)
    {
        _count = newCount;
        Render();
    }
}
