using UnityEngine;

public class Exit : MonoBehaviour
{
    public LevelQuad next;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Exit triggered : Player go to {next}");
            GameManager.Instance.GetCurrentLevelQuad();
            CameraManager.Instance().FocusOnLevelQuad(next);
            other.GetComponent<SwapMask>().SetPosition(next.GetSpawnPosition());
        }  
    }
}