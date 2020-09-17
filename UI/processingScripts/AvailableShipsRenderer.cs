using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvailableShipsRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _yellowShip;
    [SerializeField] private TextMeshProUGUI _blueShip;
    [SerializeField] private TextMeshProUGUI _greenShip;
    [SerializeField] private TextMeshProUGUI _minerShip;

    private void FixedUpdate()
    {
        _yellowShip.text = AvailableShips.YellowShips.ToString();
        _blueShip.text = AvailableShips.BlueShips.ToString();
        _greenShip.text = AvailableShips.GreenShips.ToString();
        _minerShip.text = AvailableShips.MinerShips.ToString();
    }
}
