using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // Reference to the pause menu GameObject
    // Start is called before the first frame update

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1; // Go back to the main menu
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void Restart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
