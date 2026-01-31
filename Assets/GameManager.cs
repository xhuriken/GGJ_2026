using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance() => instance;

    private LevelQuad[] quads;
    private List<Maskman> maskmen;
    private List<Mask> masks;
    private Transform player;

    void Awake()
    {
        if(instance == null && instance != this)
        {
            Destroy(instance);
            Debug.Log("GameManager duplicate destroyed");
        }
        instance = this;
        player = FindFirstObjectByType<SwapMask>().transform;
        quads = FindObjectsByType<LevelQuad>(FindObjectsSortMode.None);
        maskmen = FindObjectsByType<Maskman>(FindObjectsSortMode.None).ToList();
        masks = maskmen.Select(m => m.mask).Distinct().ToList();

    }

    public List<Maskman> GetAllMaskmen() => maskmen;
    public List<Mask> GetAllMasks() => masks;  

    public void ActivateQuadBoundaries(bool active)
    {
        quads.ForEach(quad => quad.Active = active);
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
