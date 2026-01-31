using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DearhPanel : MonoBehaviour
{
    private static DearhPanel instance;
    public static DearhPanel Instance() => instance;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
            Debug.Log("Duplicate of DeathPanel destroyed");
        }
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        Hide();
    }

    public void ReloadScene()
    {
        Hide();
        GlobalEffect.Instance.LaunchScreenTransition();
        Scene currentScene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(currentScene.name);
    }

    public void Hide()
    {
        InputSystem.Instance().OnHold = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void Show()
    {
        InputSystem.Instance().OnHold = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
}
