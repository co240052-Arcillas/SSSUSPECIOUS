using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void OpenLevel(string levelName)
    {
        // Loads a scene based on the provided level name
        SceneManager.LoadScene(levelName);
    }

    public void OpenEasyLevel()
    {
        // Loads the Easy level scene
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenMediumLevel()
    {
        // Loads the Medium level scene
        SceneManager.LoadScene("MediumScene");
    }

    public void OpenHardLevel()
    {
        // Loads the Hard level scene
        SceneManager.LoadScene("HardScene");
    }
}
