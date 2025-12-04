using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    public void OpenLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);   // Loads any level by name
    }

    public void OpenEasyLevel()
    {
        
        SceneManager.LoadScene("SampleScene"); // Load the Easy level
    }
    public void OpenMediumLevel()
    {
        
        SceneManager.LoadScene("MediumScene");  // Load the Medium level
    }
    public void OpenHardLevel()
    {
        
        SceneManager.LoadScene("HardScene"); // Load the Hard level
    }
}
