using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // UI panel for the pause menu


    public void Pause()
    {
        // Show pause menu and stop game time
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        // Hide pause menu and resume game time
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        // Return to main menu scene
        Time.timeScale = 1f;
        GameManager.Instance.MainMenu();
    }

    public void Restart()
    {
        // Restart the current gameplay scene
        Time.timeScale = 1f;
        GameManager.Instance.RestartGame();
    }
}
