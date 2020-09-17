using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarshipTypes : MonoBehaviour
{
    [SerializeField] List<Warship> _shipTypes;
    public List<Warship> ShipTypes => _shipTypes;
    private void OnValidate()
    {
        //foreach(var item in _shipTypes)
        //{
        //    IWarship go = null;
        //    if (!item.TryGetComponent<IWarship>(out go))
        //    {
        //        _shipTypes.Remove(item);
        //    }
        //}
    }
}
