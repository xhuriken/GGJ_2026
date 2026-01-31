using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;

public class CameraManager : BeatListen
{
    private static CameraManager instance;

    public static CameraManager Instance() => instance;

    private CinemachineVirtualCamera cineCam;

    [Header("Anim Settings")]
    [SerializeField] private float defaultSize = 5.3f;
    [SerializeField] private float zoomIntensity = 0.1f;
    [SerializeField] private float pulseDuration = 0.05f;





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
        GlobalEffect.Instance.LaunchScreenTransition();

    }

    protected override void HandleBeat(int beatNumber)
    {
        DOTween.Kill(cineCam);

        cineCam.m_Lens.OrthographicSize = defaultSize + zoomIntensity;

        DOTween.To(() => cineCam.m_Lens.OrthographicSize,
                   x => cineCam.m_Lens.OrthographicSize = x,
                   defaultSize,
                   pulseDuration)
            .SetEase(Ease.OutQuad)
            .SetLoops(2, LoopType.Yoyo) 
            .SetTarget(cineCam);
    }
}

