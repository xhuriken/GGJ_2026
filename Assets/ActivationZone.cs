using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    float worldRadius;

    void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        worldRadius = (spriteRenderer.bounds.min -  spriteRenderer.bounds.max).magnitude / 2f;
    }

    public float GetWorldRadius()
    {
        return worldRadius;
    }
}
