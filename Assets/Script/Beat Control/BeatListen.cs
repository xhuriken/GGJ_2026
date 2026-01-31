using UnityEngine;
using System.Collections;
using DG.Tweening;

public enum BeatType { Beat, Snare, Offbeat, OpenHat }

public abstract class BeatListen : MonoBehaviour
{
    public BeatType beatType = BeatType.Beat; 


    public IEnumerator Start()
    {
        yield return new WaitUntil(() => BeatManager.Instance != null);
        Subscribe();
    }

    void OnEnable()
    {
        if (BeatManager.Instance != null)
            Subscribe();

    }

    void OnDisable()
    {
        if (BeatManager.Instance != null)
            Unsubscribe();
    }

    void Subscribe()
    {
        switch (beatType)
        {
            case BeatType.Beat:
                BeatManager.Instance.OnBeat += HandleBeat;
                break;
            case BeatType.Snare:
                BeatManager.Instance.OnSnareBeat += HandleBeat;
                break;
            case BeatType.Offbeat:
                BeatManager.Instance.OnOffbeat += HandleBeat;
                break;
            case BeatType.OpenHat:
                BeatManager.Instance.OnOpenHat += HandleBeat;
                break;
        }
    }

    void Unsubscribe()
    {
        switch (beatType)
        {
            case BeatType.Beat:
                BeatManager.Instance.OnBeat -= HandleBeat;
                break;
            case BeatType.Snare:
                BeatManager.Instance.OnSnareBeat -= HandleBeat;
                break;
            case BeatType.Offbeat:
                BeatManager.Instance.OnOffbeat -= HandleBeat;
                break;
            case BeatType.OpenHat:
                BeatManager.Instance.OnOpenHat -= HandleBeat;
                break;
        }
    }

    protected abstract void HandleBeat(int beatNumber);
}
