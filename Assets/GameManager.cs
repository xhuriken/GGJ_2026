using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance() => instance;

    public LevelQuad[] quads;
    public Transform player;

    void Awake()
    {
        if(instance == null && instance != this)
        {
            Destroy(instance);
            Debug.Log("GameManager duplicate destroyed");
        }
        instance = this;
    }

    void Start()
    {
        player = FindFirstObjectByType<SwapMask>().transform;
        quads = FindObjectsByType<LevelQuad>(FindObjectsSortMode.None);
    }


    public LevelQuad GetCurrentLevelQuad()
    {
        foreach( var quad in quads)
        {
            if (quad.Contains(player))
            {
                return quad;
            }
        }
        Debug.LogError("player inside no quads");
        return null;
    }
    
}
