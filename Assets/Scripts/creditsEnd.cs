using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEnd : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        // normalizedTime >= 1 means the animation has played through once
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            !animator.IsInTransition(0))
        {
            SceneManager.LoadScene("TitlePage"); // replace with your menu scene name
        }
    }
}
