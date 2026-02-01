using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapMask : MonoBehaviour
{
    [SerializeField] private bool aimAssist;
    [SerializeField] private float pushSmoothTime;
    [SerializeField] private ActivationZone activationZone;
    private List<Maskman> maskmen;
    public Vector3 currentTargetPos;

    void Awake()
    {
        maskmen = GameObject
            .FindObjectsByType<Maskman>(FindObjectsSortMode.None)
            .ToList();

    }

    void Start()
    {
        InputSystem.Instance().onChangeMask.AddListener(MaskSwap);   
    }

    void OnDestroy( )
    {
        InputSystem.Instance().onChangeMask.RemoveListener(MaskSwap);
    }

    public Vector3 GetCurrentTargetPos() => currentTargetPos;

    public void SetCurrentTargetPos(Vector3 targetPos)
    {
        this.currentTargetPos = GetFinalTargetPos(targetPos);
        DOTween.Kill(this);
        transform.DOMove(this.currentTargetPos, pushSmoothTime).SetEase(Ease.OutQuart).SetTarget(this);
    }

    public void SetPosition(Vector3 targetPos)
    {
        StartCoroutine(SetPosCoroutine(targetPos));
    }

    private Vector3 GetFinalTargetPos(Vector3 targetPos)
    {
        var finalTargetPos = targetPos;
        Debug.Log($"raw : {finalTargetPos}");
        if (aimAssist)
        {
            finalTargetPos = new Vector3(
                Mathf.Round(targetPos.x + 0.5f) - 0.5f,
                Mathf.Round(targetPos.y + 0.5f) - 0.5f,
                targetPos.z
            );
            Debug.Log($"assisted : {finalTargetPos}");
        }
        return finalTargetPos;
    }

    IEnumerator SetPosCoroutine(Vector3 targetPos)
    {
        DOTween.Kill(this);
        GameManager.Instance.ActivateQuadBoundaries(false);
        enabled = false;
        yield return new WaitForFixedUpdate();
        currentTargetPos = targetPos;
        transform.position = targetPos;
        yield return new WaitForFixedUpdate();
        enabled = true;
        GameManager.Instance.ActivateQuadBoundaries(true);
    }

    public void MaskSwap(Mask mask)
    {
        Vector3 playerPos = transform.position;
        Transform pusherTransform = FindClosestMaskmanOf(mask, playerPos);
        if(pusherTransform != null)
        {
            var pushDistance = (playerPos - pusherTransform.position).normalized * mask.pushStrenght;
            currentTargetPos = pushDistance + playerPos;

            // Move maskman
            pusherTransform.GetComponent<Maskman>().Push(transform.position);
            // Move player
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
        return GameManager.Instance.GetCurrentLevelQuad().Contains(man.transform);
    }
}
