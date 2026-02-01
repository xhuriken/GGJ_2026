using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActivationZone : MonoBehaviour
{
    [SerializeField] private float worldRadius;
    private Light2D light2D;

    void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        float localSpriteRadius = spriteRenderer.sprite.bounds.extents.x;
        float scaleFactor = worldRadius / localSpriteRadius;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        light2D = GetComponent<Light2D>();
    }

    void Start()
    {
        light2D.pointLightInnerRadius = Mathf.Max(0,worldRadius - 1);
        light2D.pointLightOuterRadius = worldRadius;

    }

    public float GetWorldRadius()
    {
        return worldRadius;
    }
}
