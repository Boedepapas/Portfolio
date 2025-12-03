using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTrigger : MonoBehaviour
{
    public float delayBeforeCredits = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("LoadCredits", delayBeforeCredits);
        }
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene(2);
    }
}