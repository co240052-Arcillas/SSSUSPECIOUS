using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string SceneName = "SampleScene";

    public void PlayGame()
    {
        // Loads the main game scene asynchronously when Play is clicked
        SceneManager.LoadSceneAsync(SceneName);
    }

    public void StopGame()
    {
        // Quits the application when Stop/Exit is clicked
        Application.Quit();
    }
}
