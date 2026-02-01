using UnityEngine;

public class LevelQuad : MonoBehaviour
{
    private Transform spawn;

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
        if (other.CompareTag("Player") && Active)
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
                newTargetPos.x = newPlayerPos.x + dx;
            } 
            else if (oldPlayerPos.x - colliderRadius <= xMin) {
                newPlayerPos.x = xMax;
                newTargetPos.x = newPlayerPos.x + dx;
            }
            if (oldPlayerPos.y + colliderRadius >= yMax) {
                newPlayerPos.y = yMin;
                newTargetPos.y = newPlayerPos.y + dy;
            } 
            else if (oldPlayerPos.y - colliderRadius <= yMin) {
                newPlayerPos.y = yMax;
                newTargetPos.y = newPlayerPos.y + dy;
            }

            other.transform.position = newPlayerPos;
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
