using UnityEngine;

public class Trap : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DearhPanel.Instance().Show();
        }  
    }   
}
