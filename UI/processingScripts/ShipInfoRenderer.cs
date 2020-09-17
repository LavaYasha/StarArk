using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInfoRenderer : MonoBehaviour
{
    [Header("Ship name")]
    [SerializeField] Text _shipName;
    [Header("Hitpoint text fields")]
    [SerializeField] Slider _sliderHP;
    [SerializeField] Text _maxHP;
    [SerializeField] Text _curHP;
    [SerializeField] Text _procentHP;
    [Header("Shield text fields")]
    [SerializeField] Slider _sliderSP;
    [SerializeField] Text _maxSP;
    [SerializeField] Text _curSP;
    [SerializeField] Text _procentSP;
    [Header("Equipment fields")]
    [SerializeField] Image _weaponType;
    [SerializeField] Image _shieldType;
    [Header("Images for Highlight")]
    [SerializeField] Image _backGraund; 
    [SerializeField] Sprite _activeShip;
    [SerializeField] Sprite _selectedUnit;
    [Header("Images for type")]
    [SerializeField] List<Sprite> _equipmentTypes;
    CharInfo _info;
    float _maxHPnum;
    float _maxSPnum;
    bool choosen = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_info.IsDead)
        {
            RenderParam();
            SetActiveUnit();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCharacter(CharInfo info)
    {
        _info = info;

        _shipName.text = _info.ShipName;

        _maxHPnum = info.HitPoints;
        _maxSPnum = info.ShieldPoints;
        _maxHP.text = _maxHPnum.ToString();
        _maxSP.text = _maxSPnum.ToString();

        _weaponType.sprite = GetType(info.WeaponType);
        _shieldType.sprite = GetType(info.ShieldType);

    }

    private Sprite GetType(string type)
    {
        switch (type)
        {
            case "Blue":
                {
                    return _equipmentTypes[0];
                }
                break;
            case "Yellow":
                {
                    return _equipmentTypes[1];
                }
                break;
            case "Green":
                {
                    return _equipmentTypes[2];
                }
                break;
            default:
                {
                    return null;
                }
                break;
        }
    }

    private void RenderParam()
    {
        float procHP = _info.HitPoints / _maxHPnum;
        float procSP = _info.ShieldPoints / _maxSPnum;

        _sliderHP.value = procHP;
        _curHP.text = _info.HitPoints.ToString();
        _procentHP.text = Convert.ToInt32(procHP * 100).ToString();
        _procentHP.text += "%";

        _sliderSP.value = procSP;
        _curSP.text = _info.ShieldPoints.ToString();
        _procentSP.text = Convert.ToInt32(procSP * 100).ToString();
        _procentSP.text += "%";
    }


    private void SetActiveUnit()
    {
        var go = TurnManager.GetCurrentPlayerTurn();
        if (go != null)
        {
            if (go.name == _info.ShipName)
            {
                _backGraund.sprite = _activeShip;
            }
            else if (_info.Choosen)
            {
                _backGraund.sprite = _selectedUnit;
            }
            else
            {
                _backGraund.sprite = null;
            }
        }
    }
}
