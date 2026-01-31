using UnityEngine;
using System.Collections;
using DG.Tweening;

public enum BeatType { Beat, Snare, Offbeat, OpenHat }

public class BeatListen : MonoBehaviour
{
    public BeatType beatType = BeatType.Beat; 


    void Awake()
    {
    }

    IEnumerator Start()
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

    void HandleBeat(int beatNumber)
    {
        if (this == null)
            return;
        // MAKE SOMETHING !
    }   
}
