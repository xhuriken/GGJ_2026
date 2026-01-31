using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InputSystem : MonoBehaviour
{
    private static InputSystem instance;
    public static InputSystem Instance() => instance;

    public List<Mask> masks;
    public UnityEvent<Mask> onChangeMask; 
    public bool OnHold { get; set; } = false; 

    void Awake()
    {
        if(instance != null &&  instance != this)
        {
            Destroy(instance);
            Debug.Log($"Duplicate of InputSystem deployed");
        }
        instance  = this;
        masks = GameObject.FindObjectsByType<Maskman>(FindObjectsSortMode.None)
            .Select(man => man.mask)
            .Distinct()
            .ToList();
    }

    void Update()
    {
        if(OnHold)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DOTween.KillAll();
            Scene currentScene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(currentScene.name);
        }
        foreach(var mask in masks)
        {
            if (Input.GetKeyDown(mask.code))
            {
                onChangeMask?.Invoke(mask);

                if (BeatManager.Instance.IsOnBeat())
                {
                    Debug.Log("On Beat!");
                    // TODO: add scoring effect
                }

                onChangeMask.Invoke(mask);
                Debug.Log($"swap to {mask.label}");   
                break;
            }
        }
        
    }
}
