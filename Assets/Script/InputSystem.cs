using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    private static InputSystem instance;
    public static InputSystem Instance() => instance;

    private List<Mask> masks;
    public UnityEvent<Mask> onChangeMask; 

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
