using TMPro;
using UnityEngine;

public class Maskman : MonoBehaviour
{
    public Mask mask;
    public TextMeshProUGUI buttonLabel;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.color = mask.color;
        buttonLabel.text = mask.code.ToString();
    }

    public override string ToString()
    {
        return $"{mask.label} maskman";
    }

}