using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class BeatManager : MonoBehaviour
{
    [Header("Beat Settings")]
    [Tooltip("Beats per minute of the music!")]
    public int bpm = 128;

    public static BeatManager Instance { get; private set; }

    public event Action<int> OnBeat;
    public event Action<int> OnSnareBeat;
    public event Action<int> OnOffbeat;
    public event Action<int> OnOpenHat;
    public event Action<int> OnPhaseTrigger;

    private double startDspTime;
    private double nextBeatTime;
    private double nextOffbeatTime;

    public int beatCount;
    public int localbeatCount;
    private int measureCount;
    private int openHatCount;



    private AudioSource audioSource;
    private bool hasSlowedDown = false;

    [Header("Rythm Detection")]
    public float threshold = 0.12f;
    public double BeatDuration => 60.0 / bpm;

    private List<BeatListen> listeners = new List<BeatListen>();

    void Awake()
    {
        Time.fixedDeltaTime = 0.02f;

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.Play();

        double beatDuration = 60.0 / bpm;
        startDspTime = AudioSettings.dspTime;
        nextBeatTime = startDspTime + beatDuration;
        nextOffbeatTime = startDspTime + beatDuration * 0.5;
        beatCount = 0;
        measureCount = 0;
        openHatCount = 0;
        localbeatCount = 0;
        StartCoroutine(BeatLoop());
        StartCoroutine(OffbeatLoop());
        StartCoroutine(OpenHatLoop());
    }

    public void RegisterListener(BeatListen listener)
    {
        if (!listeners.Contains(listener)) listeners.Add(listener);
    }

    public void UnregisterListener(BeatListen listener)
    {
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }
    IEnumerator BeatLoop()
    {
        double beatDuration = 60.0 / bpm;

        while (true)
        {
            yield return null;

            if (AudioSettings.dspTime >= nextBeatTime)
            {
                localbeatCount++;
                beatCount++;
                foreach (var listener in listeners.ToArray())
                {
                    if (listener != null && listener.beatType == BeatType.Beat)
                        listener.HandleBeat(beatCount);
                }

                if ((beatCount % 4) == 0)
                {
                    OnSnareBeat?.Invoke(beatCount);
                    measureCount++;
                    //Debug.LogWarning($"[BeatManager] {measureCount} mesure");
                }

                nextBeatTime += beatDuration;
            }
        }
    }

    IEnumerator OffbeatLoop()
    {
        double beatDuration = 60.0 / bpm;
        int offbeatCount = 0;

        while (true)
        {
            yield return null;

            if (AudioSettings.dspTime >= nextOffbeatTime)
            {
                offbeatCount++;
                foreach (var listener in listeners.ToArray())
                {
                    if (listener != null && listener.beatType == BeatType.Offbeat)
                        listener.HandleBeat(beatCount);
                }
                nextOffbeatTime += beatDuration;
            }
        }
    }

    IEnumerator OpenHatLoop()
    {
        double beatDuration = 60.0 / bpm;

        while (true)
        {
            yield return null;

            double timeSinceStart = AudioSettings.dspTime - startDspTime;
            double currentBeatPosition = timeSinceStart / beatDuration;
            double beatInMeasure = currentBeatPosition % 4.0;

            if (beatInMeasure >= 2.7 && beatInMeasure < 2.75)
            {
                if (openHatCount < measureCount)
                {
                    openHatCount = measureCount;
                    foreach (var listener in listeners.ToArray())
                    {
                        if (listener != null && listener.beatType == BeatType.OpenHat)
                            listener.HandleBeat(beatCount);
                    }
                }
            }
        }
    }

    public void PitchAndLaunchScene(int SceneId ,bool quitgame = false)
    {
        TriggerEndSlowMotion(SceneId, quitgame);
    }

    public void TriggerEndSlowMotion(int SceneId, bool quitgame = false)
    {

        float duration = 1f;

        //timescale
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, duration)
            .SetEase(Ease.InOutSine);
        //pitch
        DOTween.To(() => audioSource.pitch, x => audioSource.pitch = x, 0f, duration)
            .SetEase(Ease.InOutSine);

        StartCoroutine(FadeToBlackThenLoad(SceneId, quitgame));
    }

    IEnumerator FadeToBlackThenLoad(int SceneId, bool quitgame = false)
    {
        yield return new WaitForSecondsRealtime(1f);

        //Vignette PostProcess to black
        //Intensity to max
        //    Smoothnes to max in 1sec


        // TODO: Fade out transition
        yield return new WaitForSecondsRealtime(1f);
        

        Time.timeScale = 1f;
        audioSource.pitch = 1f;
        DOTween.KillAll(true);
        yield return new WaitForSecondsRealtime(1f);
        if(quitgame)
        {
            Application.Quit();
            yield break;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneId);

        
    }

    public bool IsOnBeat()
    {
        double currentTime = AudioSettings.dspTime;

        // Time elapsed from the start of the game
        double timeElapsed = currentTime - startDspTime;

        // where we are sinse le last beat
        double timeSinceLastBeat = timeElapsed % BeatDuration;

        // time until the next beat
        double timeToNextBeat = BeatDuration - timeSinceLastBeat;

        // we take the shorter time between both
        double smallestDiff = Math.Min(timeSinceLastBeat, timeToNextBeat);

        // if the time is <= treshold, WE ARE ON TIME
        return smallestDiff <= threshold;
    }

}