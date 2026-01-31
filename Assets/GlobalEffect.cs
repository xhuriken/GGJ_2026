using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalEffect : MonoBehaviour
{
    private static GlobalEffect instance;
    public static GlobalEffect Instance => instance;

    [SerializeField] private Volume postProcessVolume;
    private LensDistortion lensDistortion;

    void Awake()
    {
        // SINGLETON PROPRE
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (postProcessVolume.profile.TryGet(out lensDistortion))
        {
            lensDistortion.intensity.Override(0f);
        }
    }

    public void LaunchScreenTransition()
    {
        if (lensDistortion == null) return;
        DOTween.Kill(lensDistortion);
        lensDistortion.intensity.value = 0f;
        DOTween.To(() => lensDistortion.intensity.value,
                   x => lensDistortion.intensity.value = x,
                   -1f, 0.2f)
            .SetEase(Ease.OutExpo)
            .SetLoops(2, LoopType.Yoyo)
            .SetTarget(lensDistortion);
    }
}