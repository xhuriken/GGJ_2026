using UnityEngine;

public class LevelQuad : MonoBehaviour
{
    float xMax, yMax, xMin, yMin;
    

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

    private void OnTriggerExit2D(Collider2D other)
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
                newPlayerPos.x = xMin;
                newTargetPos.x = xMin + dx;
            } 
            else if (oldPlayerPos.x - colliderRadius <= xMin) {
                newPlayerPos.x = xMax;
                newTargetPos.x = xMax + dx;
            }
            if (oldPlayerPos.y + colliderRadius >= yMax) {
                newPlayerPos.y = yMin;
                newTargetPos.y = yMin + dy;
            } 
            else if (oldPlayerPos.y - colliderRadius <= yMin) {
                newPlayerPos.y = yMax;
                newTargetPos.y = yMax + dy;
            }

            other.transform.position = newPlayerPos;
            swapMask.SetCurrentTargetPos(newTargetPos);
        }
    }

}
