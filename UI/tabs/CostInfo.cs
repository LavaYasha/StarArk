using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostInfo : Tab
{
    [SerializeField] TextMeshProUGUI _metalCost;
    [SerializeField] TextMeshProUGUI _yellowGemCost;
    [SerializeField] TextMeshProUGUI _blueGemCost;
    [SerializeField] TextMeshProUGUI _greenGemCost;
    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out localPoint);
        localPoint.y -= 160f;
        transform.localPosition = localPoint;
    }
    public void ShowCost(int metal, int yellowGem, int blueGem, int greenGem)
    {
        _metalCost.text = "x" + metal.ToString();
        _yellowGemCost.text = "x" + yellowGem.ToString();
        _blueGemCost.text = "x" + blueGem.ToString();
        _greenGemCost.text = "x" + greenGem.ToString();
    }
    public void ShowCost(Costs cost)
    {
        ShowCost(cost.metal, cost.yellowGem, cost.blueGem, cost.greenGem);
    }
}
