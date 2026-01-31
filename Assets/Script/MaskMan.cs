using UnityEngine;

public class Maskman : MonoBehaviour
{
    public Mask mask;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.color = mask.color;
    }

    public override string ToString()
    {
        return $"{mask.label} maskman";
    }

}