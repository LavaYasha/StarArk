using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadCell : MonoBehaviour
{
    [SerializeField] Sprite _error;
    [SerializeField] Sprite _defult;
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Sprite _unavailable;
    [SerializeField] bool _available = true;
    [SerializeField] bool _filled = false;
    IWarship _ship;

    private bool changing = true;

    public IWarship Ship => _ship;
    public bool Available  => _available;

    public bool Filled => _filled; 

    private void Update()
    {
        if (changing)
        {
            if (_available && !_filled)
            {
                if (_defult == null)
                {
                    gameObject.GetComponent<Image>().color = _defaultColor;
                    gameObject.GetComponent<Image>().sprite = null;
                }
                else
                {
                    gameObject.GetComponent<Image>().sprite = _defult;
                }
                changing = false;
            }
            else if (_filled)
            {
                if (_ship == null || _ship.Icon == null)
                {
                    gameObject.GetComponent<Image>().sprite = _error;
                }
                else
                {
                    gameObject.GetComponent<Image>().sprite = _ship.Icon;
                }
                changing = false;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = _unavailable;
                changing = false;
            }
        }
    }

    public void AddUnit(IWarship ship)
    {
        if (ship != null)
        {
            _filled = true;
        }
        _ship = ship;
        changing = true;
    }

    public void RemoveUnit()
    {
        _filled = false;
        _ship = null;
        changing = true;
    }
    public void RemoveUnit(out IWarship removedShip)
    {
        removedShip = _ship;
        RemoveUnit();
    }

    public void LockCell()
    {
        _available = false;
        changing = true;
    }
}
