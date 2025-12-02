using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }

    [Header("Settings")]
    public Difficulty currentDifficulty;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI currentScoreTextTMP;
    [SerializeField] private TextMeshProUGUI highScoreTextTMP;

    private int currentScore = 0;
    private int highScore = 0;

    private void Awake()
    {
        // Loads the high score for the selected difficulty and updates the UI
        highScore = PlayerPrefs.GetInt(GetHighScoreKey(), 0);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        // Adds points to the current score, updates high score if exceeded, and refreshes the UI
        currentScore += amount;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(GetHighScoreKey(), highScore);
        }

        UpdateUI();
    }

    public void ResetScore()
    {
        // Resets the current score to zero and updates the UI
        currentScore = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Updates the displayed current score and high score in the UI
        if (currentScoreTextTMP != null)
            currentScoreTextTMP.text = $"Current Score: {currentScore}";

        if (highScoreTextTMP != null)
            highScoreTextTMP.text = $"High Score: {highScore}";
    }

    private string GetHighScoreKey()
    {
        // Returns a unique PlayerPrefs key based on the current difficulty
        return "HighScore_" + currentDifficulty.ToString();
    }

    public int GetCurrentScore()
    {
        // Returns the current score
        return currentScore;
    }

    public int GetHighScore()
    {
        // Returns the high score
        return highScore;
    }
}
