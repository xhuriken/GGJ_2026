using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BeatListen
{
    protected override void HandleBeat(int beatNumber)
    {
        //Debug.Log($"Je suis le player et yoohoo �a beat un max ici: {beatNumber}");
        DOTween.Kill(this);
        transform.DOScale(1.2f, 0.05f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutQuad);

    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
