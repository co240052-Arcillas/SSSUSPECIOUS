using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private const string SceneName = "SampleScene"; // Default scene to load


    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneName);   // Loads the main game scene
    }

    public void StopGame() // Closes the application
    {
        Application.Quit();
    }
}
