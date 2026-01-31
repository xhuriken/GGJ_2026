using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapMask : MonoBehaviour
{
    [SerializeField] private float strenght;
    [SerializeField] private float smoothTime;
    [SerializeField] private ActivationZone activationZone;
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

    public void SetPosition(Vector3 targetPos)
    {
       StartCoroutine(SetPosCoroutine(targetPos));
    }

    IEnumerator SetPosCoroutine(Vector3 targetPos)
    {
        DOTween.Kill(this);
        GameManager.Instance().ActivateQuadBoundaries(false);
        enabled = false;
        yield return new WaitForFixedUpdate();
        currentTargetPos = targetPos;
        transform.position = targetPos;
        yield return new WaitForFixedUpdate();
        enabled = true;
        GameManager.Instance().ActivateQuadBoundaries(true);
    }

    public void MaskSwap(Mask mask)
    {
        Vector3 playerPos = transform.position;
        Transform pusherTransform = FindClosestMaskmanOf(mask, playerPos);
        if(pusherTransform != null)
        {
            var pushDistance = (playerPos - pusherTransform.position).normalized * strenght;
            currentTargetPos = pushDistance + playerPos;
            SetCurrentTargetPos(currentTargetPos);
        }
    }   

    private Transform FindClosestMaskmanOf(Mask mask, Vector3 playerPos)
    {   
        float minDistance = float.MaxValue;
        Transform pusherTransform = null;
        foreach(var maskman in maskmen)
        {
            if(!maskman.mask.Equals(mask))
            {
                continue;
            }

            if(IsOutsideActivationZone(maskman))
            {
                continue;
            }

            if (!IsInCurrentLevelQuad(maskman))
            {
                continue;
            }

            var distance = (maskman.transform.position - playerPos).magnitude;
            if(distance < minDistance)
            {
                pusherTransform = maskman.transform;
                minDistance = distance;
            }
        }
        return pusherTransform ;
    }

    private bool IsOutsideActivationZone(Maskman man)
    {
        var distToMaskman = (man.transform.position - transform.position).magnitude;
        return distToMaskman > activationZone.GetWorldRadius();
    }

    private bool IsInCurrentLevelQuad(Maskman man)
    {
        return GameManager.Instance().GetCurrentLevelQuad().Contains(man.transform);
    }
}
