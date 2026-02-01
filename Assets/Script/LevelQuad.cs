using UnityEngine;

public class LevelQuad : MonoBehaviour
{
    private Transform spawn;
    public float colliderRadius;

    float xMax, yMax, xMin, yMin;

    public float GetXMax() => xMax;
    public float GetXmin() => xMin;
    public float GetYMax() => yMax; 
    public float GetYmin() => yMin; 
    public Vector3 GetSpawnPosition() => spawn.transform.position;

    public bool Active { get; set;} = true;

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Bounds bounds = spriteRenderer.bounds;

        xMin = bounds.min.x; // Left edge
        xMax = bounds.max.x; // Right edge
        yMin = bounds.min.y; // Bottom edge
        yMax = bounds.max.y; // Top edge

        Debug.Log($"Boundaries Calculated: X({xMin}, {xMax}) Y({yMin}, {yMax})");

        GameObject[] spawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        foreach (GameObject s in spawns)
        {
            if (Contains(s.transform))
            {
                this.spawn = s.transform;
                break;
            }
        }
    }

    public Vector3 GetWorldCenter()
    {
        return new Vector3(xMax - xMin, yMax - yMin, transform.position.z);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // do not use
    }

    public void HandleBorderCrossing(Transform transform)
    {
        if (transform.CompareTag("Player") && Active)
        {
            Debug.Log("Player has left the level!");    
            SwapMask swapMask = transform.gameObject.GetComponent<SwapMask>();
            Vector3 newTargetPos = swapMask.GetCurrentTargetPos();
            Vector3 oldPlayerPos = transform.position;
            Vector3 newPlayerPos = new (
                transform.position.x,
                transform.position.y,
                transform.position.z
                );
            float dx = newTargetPos.x - newPlayerPos.x;
            float dy = newTargetPos.y - newPlayerPos.y;
            float enterFactor = 1.5f;
            if (oldPlayerPos.x + colliderRadius >= xMax) {
                newPlayerPos.x = xMin + enterFactor * colliderRadius;
                newTargetPos.x = newPlayerPos.x + dx;
            } 
            else if (oldPlayerPos.x - colliderRadius <= xMin) {
                newPlayerPos.x = xMax - enterFactor * colliderRadius;
                newTargetPos.x = newPlayerPos.x + dx;
            }
            if (oldPlayerPos.y  + colliderRadius >= yMax) {
                newPlayerPos.y = yMin  + enterFactor * colliderRadius;
                newTargetPos.y = newPlayerPos.y + dy;
            } 
            else if (oldPlayerPos.y -  colliderRadius <= yMin) {
                newPlayerPos.y = yMax  - enterFactor * colliderRadius;
                newTargetPos.y = newPlayerPos.y + dy;
            }

            transform.position = newPlayerPos;
            swapMask.SetCurrentTargetPos(newTargetPos);
        }
    }

    public bool Contains(Transform transform)
    {
        return xMin - 0.01f <= transform.position.x 
            && yMin - 0.01f <= transform.position.y 
            && xMax + 0.01f >= transform.position.x 
            && yMax + 0.01f >= transform.position.y ;
    }

}
