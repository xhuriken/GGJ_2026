using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        yield return null;
        // TODO: load the main scene
    }

    public void Quit()
    {
        StartCoroutine(QuitTransition());
    }

    private IEnumerator QuitTransition()
    {

        yield return null;
        Application.Quit();

    }
}
