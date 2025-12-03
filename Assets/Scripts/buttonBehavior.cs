using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene loading
using UnityEngine.UI;              // Needed if you want to hook directly to a Button

public class LoadSceneButton : MonoBehaviour
{
    // Name of the scene to load (set in Inspector)
    public string sceneName;
    public int sceneIndex;

    void Start()
    {
        // If this script is attached to a Button, hook up the click event
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(LoadScene);
        }
    }

    // Function that loads the scene
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}