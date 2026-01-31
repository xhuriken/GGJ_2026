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

    [Header("Components")]
    [SerializeField] private Volume postProcessVolume;
    private LensDistortion lensDistortion;



    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
            Debug.Log("Duplicate of CameraManager destroyed");
        }
        instance = this;
        cineCam = GetComponent<CinemachineVirtualCamera>();

        if (postProcessVolume.profile.TryGet(out lensDistortion))
        {
            lensDistortion.intensity.Override(0f);
        }
    }

    public void FocusOnLevelQuad(LevelQuad levelQuad)
    {
        cineCam.LookAt = levelQuad.transform;
        cineCam.Follow = levelQuad.transform;

        // Get volume, tween values
        // Get lens distortion, move intensity
        if (lensDistortion != null)
        {
            DOTween.Kill(lensDistortion);
            DOTween.To(() => lensDistortion.intensity.value,
                       x => lensDistortion.intensity.value = x,
                       -1,
                       0.2f)
                .SetEase(Ease.OutExpo) // Plus "punchy" pour la distorsion
                .SetLoops(2, LoopType.Yoyo)
                .SetTarget(lensDistortion);
        }

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

