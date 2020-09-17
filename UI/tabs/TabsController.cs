using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabsController : MonoBehaviour
{
    [SerializeField] Tab _escTab;
    [SerializeField] Tab _f1Tab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_f1Tab._isOpen)
            {
                _escTab.Toggle();
            }
            else
            {
                _f1Tab.Hide();
            }
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _f1Tab.Toggle();
        }
    }
}
