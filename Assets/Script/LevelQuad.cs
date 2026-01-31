using UnityEngine;

public class LevelQuad : MonoBehaviour
{
    public LevelQuad left;
    public LevelQuad right;
    public LevelQuad top;
    public LevelQuad bottom;

    float xMax, yMax, xMin, yMin;

    public float GetXMax() => xMax;
    public float GetXmin() => xMin;
    public float GetYMax() => yMax; 
    public float GetYmin() => yMin; 

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Bounds bounds = spriteRenderer.bounds;

        xMin = bounds.min.x; // Left edge
        xMax = bounds.max.x; // Right edge
        yMin = bounds.min.y; // Bottom edge
        yMax = bounds.max.y; // Top edge

        Debug.Log($"Boundaries Calculated: X({xMin}, {xMax}) Y({yMin}, {yMax})");
    }

    public Vector3 GetWorldCenter()
    {
        return new Vector3(xMax - xMin, yMax - yMin, transform.position.z);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has left the level!");
            CircleCollider2D collider = other.gameObject.GetComponent<CircleCollider2D>();
            float colliderRadius = collider.radius;
            SwapMask swapMask = other.gameObject.GetComponent<SwapMask>();
            Vector3 newTargetPos = swapMask.GetCurrentTargetPos();
            Vector3 oldPlayerPos = other.transform.position;
            Vector3 newPlayerPos = new (
                other.transform.position.x,
                other.transform.position.y,
                other.transform.position.z
                );
            float dx = newTargetPos.x - newPlayerPos.x;
            float dy = newTargetPos.y - newPlayerPos.y;

            if (oldPlayerPos.x + colliderRadius >= xMax) {
                
                newPlayerPos.x = right ? right.GetXmin() : xMin;
                newTargetPos.x = newPlayerPos.x + dx;
                if (right)
                {
                    CameraManager.Instance().FocusOnLevelQuad(right);
                }

            } 
            else if (oldPlayerPos.x - colliderRadius <= xMin) {
                newPlayerPos.x = left ? left.GetXMax()  : xMax;
                newTargetPos.x = newPlayerPos.x + dx;
                if (left)
                {
                    CameraManager.Instance().FocusOnLevelQuad(left);
                }
            }
            if (oldPlayerPos.y + colliderRadius >= yMax) {
                newPlayerPos.y = bottom ? bottom.GetYmin() : yMin;
                newTargetPos.y = newPlayerPos.y + dy;
                if (bottom)
                {
                    CameraManager.Instance().FocusOnLevelQuad(bottom);
                }
            } 
            else if (oldPlayerPos.y - colliderRadius <= yMin) {
                newPlayerPos.y = top ? top.GetYMax() : yMax;
                newTargetPos.y = newPlayerPos.y + dy;
                if (top)
                {
                    CameraManager.Instance().FocusOnLevelQuad(top);
                }
            }

            other.transform.position = newPlayerPos;
            swapMask.SetCurrentTargetPos(newTargetPos);
        }
    }

}
