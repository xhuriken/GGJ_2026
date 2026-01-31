using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    [SerializeField] private float worldRadius;

    void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        float localSpriteRadius = spriteRenderer.sprite.bounds.extents.x;
        float scaleFactor = worldRadius / localSpriteRadius;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }

    public float GetWorldRadius()
    {
        return worldRadius;
    }
}
