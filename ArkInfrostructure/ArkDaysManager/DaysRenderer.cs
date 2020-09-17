using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaysRenderer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentDay;
    
    public void RenderCurrentDay(int curDay)
    {
        int firtDigit = curDay / 1000;
        int secDigit = (curDay / 100) % 10;
        int therdDigit = (curDay / 10) % 10;
        int forthDigit = curDay % 10;
        _currentDay.text ="Day: " + firtDigit.ToString() + secDigit.ToString() + therdDigit.ToString() + forthDigit.ToString();
    }
}
