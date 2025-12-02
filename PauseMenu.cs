using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        // Activates the pause menu and stops game time
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        // Hides the pause menu and resumes game time
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        // Resumes game time and navigates to the main menu
        Time.timeScale = 1f;
        GameManager.Instance.MainMenu();
    }

    public void Restart()
    {
        // Resumes game time and restarts the current game
        Time.timeScale = 1f;
        GameManager.Instance.RestartGame();
    }
}
