using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int nextSceneBuildIndex = -1; // Set in Inspector, or leave -1 to auto-next

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (nextSceneBuildIndex >= 0)
            {
                SceneManager.LoadScene(nextSceneBuildIndex);
            }
            else
            {
                // Load the next scene in build order
                int current = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(current + 1);
            }
        }
    }
}