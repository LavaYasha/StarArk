using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RenderResourses : MonoBehaviour
{
    [Header("Resourses count")]
    [SerializeField] TextMeshProUGUI _metal;
    [SerializeField] TextMeshProUGUI _yellowGem;
    [SerializeField] TextMeshProUGUI _blueGem;
    [SerializeField] TextMeshProUGUI _greenGem;
    [Header("Resourses income")]
    [SerializeField] TextMeshProUGUI _metalIncome;
    [SerializeField] TextMeshProUGUI _yellowGemIncome;
    [SerializeField] TextMeshProUGUI _blueGemIncome;
    [SerializeField] TextMeshProUGUI _greenGemIncome;
    void FixedUpdate()
    {
        RenderResoursesCount();
        RenderResoursesIncome();
    }

    private void RenderResoursesCount()
    {
        _metal.text = Werehouses.Metal.ToString();
        _yellowGem.text = Werehouses.YellowGem.ToString();
        _blueGem.text = Werehouses.BlueGem.ToString();
        _greenGem.text = Werehouses.GreenGem.ToString();
    }

    private void RenderResoursesIncome()
    {
        Werehouses.CalculateIncome();
        _metalIncome.text = $" + {Werehouses.MetalIncome.ToString()}p/d";
        _yellowGemIncome.text = $" + {Werehouses.YellowGemIncome.ToString()}p/d";
        _blueGemIncome.text = $" + {Werehouses.BlueGemIncome.ToString()}p/d";
        _greenGemIncome.text = $" + {Werehouses.GreenGemIncome.ToString()}p/d";
    }

}
