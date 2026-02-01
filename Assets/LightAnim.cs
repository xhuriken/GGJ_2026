using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnim : BeatListen
{
    private Animator animator;

    public override void HandleBeat(int beatNumber)
    {
        Debug.Log("A light received a beat");

        if (animator != null)
        {
            animator?.SetTrigger("Beat");
            Debug.Log("Set trigger of a light");
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
