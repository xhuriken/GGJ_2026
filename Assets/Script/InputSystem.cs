using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    private List<Mask> masks;
    public UnityEvent<Mask> onChangeMask; 

    void Awake()
    {
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
                Debug.Log($"swap to {mask.label}");   
                break;
            }
        }
        
    }
}
