using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {

        GlobalEffect.Instance.LaunchScreenTransition();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        StartCoroutine(QuitTransition());
    }

    private IEnumerator QuitTransition()
    {
        GlobalEffect.Instance.LaunchScreenTransition();
        yield return new WaitForSeconds(0.5f);
        Application.Quit();

    }
}
