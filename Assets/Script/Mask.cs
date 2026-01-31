using UnityEngine;

[CreateAssetMenu(fileName = "Mask", menuName = "MyLib/Mask")]
public class Mask : ScriptableObject
{
    public Color color;
    public KeyCode code;
    public string label;
}