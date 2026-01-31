using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaskLegend : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonLabel;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetContent(Mask mask)
    {
        image.color = mask.color;
        buttonLabel.text = mask.code.ToString();
    }
}
