using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    public static CameraManager Instance() => instance;

    private CinemachineVirtualCamera cineCam;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
            Debug.Log("Duplicate of CameraManager destroyed");
        }
        instance = this;
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void FocusOnLevelQuad(LevelQuad levelQuad)
    {
        cineCam.LookAt = levelQuad.transform;
        cineCam.Follow = levelQuad.transform;
    }
}
