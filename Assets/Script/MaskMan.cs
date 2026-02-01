using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Maskman : BeatListen
{
    public Mask mask;
    public TextMeshProUGUI buttonLabel;

    [Header("Bounce Settings")]
    [SerializeField] private Vector3 Scale = new Vector3(1.2f, 1.2f, 1.2f); 
    [SerializeField] private float duration = 0.2f;

    private GameObject sprite;
    private Animator animator;
    private Vector3 baseScale;

    private void Awake()
    {
        sprite = transform.GetChild(0).gameObject;
        animator = sprite.GetComponent<Animator>();
        baseScale = sprite.transform.localScale;
    }

    void Start()
    {
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

    protected override void HandleBeat(int beatNumber)
    {
        Debug.Log($"{gameObject.name} Zbi");

        animator.SetTrigger("Beat");    
        DOTween.Kill(this);
        sprite.transform.localScale = Scale; 
        sprite.transform.DOScale(baseScale, duration).SetTarget(this);


    }
}