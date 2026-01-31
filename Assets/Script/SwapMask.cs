using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapMask : MonoBehaviour
{
    [SerializeField] private float strenght;
    [SerializeField] private float smoothTime;
    private List<Maskman> maskmen;
    public Vector3 currentTargetPos;

    void Awake()
    {
        maskmen = GameObject
            .FindObjectsByType<Maskman>(FindObjectsSortMode.None)
            .ToList();
    }

    public Vector3 GetCurrentTargetPos() => currentTargetPos;

    public void SetCurrentTargetPos(Vector3 currentTargetPos)
    {
        this.currentTargetPos = currentTargetPos;
        DOTween.Kill(this);
        transform.DOMove(currentTargetPos, smoothTime).SetEase(Ease.OutQuart).SetTarget(this);
    }

    public void MaskSwap(Mask mask)
    {
        Vector3 playerPos = transform.position;
        Vector3 pusherPOs = FindClosestMaskmanOf(mask, playerPos);
        var pushDistance = (playerPos - pusherPOs).normalized * strenght;
        currentTargetPos = pushDistance + playerPos;
        SetCurrentTargetPos(currentTargetPos);
    }   

    private Vector3 FindClosestMaskmanOf(Mask mask, Vector3 playerPos)
    {   
        float minDistance = float.MaxValue;
        Vector3 pusherPos = Vector3.zero;
        foreach(var maskman in maskmen)
        {
            if(!maskman.mask.Equals(mask))
            {
                continue;
            }

            var distance = (maskman.transform.position - playerPos).magnitude;
            if(distance < minDistance)
            {
                pusherPos = maskman.transform.position;
                minDistance = distance;
            }
        }
        return pusherPos;
    }

}
