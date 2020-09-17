using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DebugGameCam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera groupCam;
    [SerializeField] private CinemachineVirtualCamera gameCam;

    void Update()
    {
        
    }
    public void GroupCam()
    {
        groupCam.Priority = 10;
        gameCam.Priority = 0;
        
    }
    public void GameCam()
    {
        groupCam.Priority = 0;
        gameCam.Priority = 10;
        
    }
}
