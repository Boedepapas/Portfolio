using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class creditsEnd : MonoBehaviour
{
    private bool creditFinished = false;
    private RectTransform rect;
    public float creditEndY;   // target anchored Y position
    public float speed;        // scroll speed

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!creditFinished)
        {
            // Move the credits upward in anchored space
            Vector2 pos = rect.anchoredPosition;
            pos.y += speed * Time.deltaTime;

            // Clamp so it never goes past the end
            if (pos.y >= creditEndY)
            {
                pos.y = creditEndY;   // snap exactly to end
                creditFinished = true;
            }

            rect.anchoredPosition = pos;
        }
        else
        {
            // Credits finished, wait for any input
            if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
                
            {
                SceneManager.LoadScene("TitlePage"); // replace with your actual scene name
            }
        }
    }
}