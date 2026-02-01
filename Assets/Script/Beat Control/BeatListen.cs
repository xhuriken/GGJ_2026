using UnityEngine;
using System.Collections;
using DG.Tweening;

public enum BeatType { Beat, Snare, Offbeat, OpenHat }

public abstract class BeatListen : MonoBehaviour
{
    public BeatType beatType = BeatType.Beat;


    void OnEnable()
    {
        StartCoroutine(WaitAndSubscribe());
    }

    IEnumerator WaitAndSubscribe()
    {
        // On attend que l'instance soit là
        yield return new WaitUntil(() => BeatManager.Instance != null);
        BeatManager.Instance.RegisterListener(this);
    }

    void OnDisable()
    {
        if (BeatManager.Instance != null)
            BeatManager.Instance.UnregisterListener(this);
    }

    public abstract void HandleBeat(int beatNumber);
}
