using UnityEngine;

public class PlayerQuadInteraction : MonoBehaviour
{
    private LevelQuad currentQuad;

    void Start()
    {
        currentQuad = GameManager.Instance.GetCurrentLevelQuad();
    }

    public void SetCurrentQuad(LevelQuad next)
    {
        currentQuad = next;
    }
    
    void Update()
    {
        if (!currentQuad.Contains(transform))
        {
            currentQuad.HandleBorderCrossing(transform);
        }
    }

}
