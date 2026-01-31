using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Maskman : MonoBehaviour
{
    public Mask mask;
    public TextMeshProUGUI buttonLabel;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.color = mask.color;
        buttonLabel.text = mask.code.ToString();
    }

    public override string ToString()
    {
        return $"{mask.label} maskman";
    }

    internal void Push(Vector3 position)
    {
        Vector3 basePos = transform.position;
        // Move to the position quickly
        transform.DOMove(position, 0.05f).SetEase(Ease.OutElastic).SetTarget(this).OnComplete(() =>
        {
            // come back chilly
            transform.DOMove(basePos, 0.2f).SetEase(Ease.OutSine).SetTarget(this);
        });

    }
}