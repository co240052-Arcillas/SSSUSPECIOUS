using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverScreen;

    private void Awake()
    {
        // Sets up the singleton instance for global access
        Instance = this;
    }

    public void GameOver()
    {
        // Activates the game over screen and stops game time
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Resumes game time and reloads the current scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Resumes game time and loads the main menu scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
