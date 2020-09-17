using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipBuildingCosts : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] ShipBuildings _build;
    [SerializeField] string ShipType;
    [SerializeField] Costs _cost;
    [SerializeField] CostInfo tooltipe;
    private void Start()
    {
        
        Button button = gameObject.GetComponent<Button>();
        
        button.onClick.AddListener(() =>
        {
            _build.AddShipToConstruct(ShipType, _cost);
        });
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipe.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipe.Show();
        tooltipe.ShowCost(_cost);
    }
}
[Serializable]
public struct Costs
{
    public int metal;
    public int yellowGem;
    public int blueGem;
    public int greenGem;
}