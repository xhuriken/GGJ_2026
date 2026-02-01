using UnityEngine;
using UnityEngine.UI;

public class MaskLegendPanel : MonoBehaviour
{
    [SerializeField] private GameObject maskLegend;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        foreach(var mask in GameManager.Instance.GetAllMasks())
        {
            var go = Instantiate(maskLegend, transform, false);
            go.GetComponent<MaskLegend>().SetContent(mask);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
}
