using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : CustomButtonBase
{

    public Image _image;
    private RectTransform _rectTransform;

    private Tween _tweenEnter;
    private Tween _tweenExit;
    private Tween _tweenClick;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        _tweenEnter.Kill();
        _tweenEnter = _image.DOColor(Color.red, 0.2f).SetEase(Ease.InOutCubic).SetTarget(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        _tweenExit.Kill();
        _tweenExit = _image.DOColor(Color.white, 0.2f).SetEase(Ease.InOutCubic).SetTarget(this);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        // TODO: play SFX
        _rectTransform.localScale = Vector3.one; 
        _tweenClick.Kill();
        _tweenClick = _rectTransform.DOScale(0.7f, 0.1f).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo).SetTarget(this);
    }
}
