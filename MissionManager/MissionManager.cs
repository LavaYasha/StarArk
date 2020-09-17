using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] GameObject _contentBuffer;
    [SerializeField] GameObject _missionTemplate;
    private void CalculateChanceMissionSpawn()
    {
        float num = Random.Range(0, 100);
        if(num < 10)
        {
            CreateMission();
        }
    }

    private void CreateMission()
    {
        Instantiate(_missionTemplate, _contentBuffer.transform);
    }
}
