using System.Collections;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;    // Assign TMP text in Inspector
    [SerializeField] private float startCount = 3f; // Countdown starts at 3

    public bool CanStart { get; private set; } = false; // Snake can move only if true

    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true); // Make sure the countdown text is visible
            StartCoroutine(CountdownCoroutine());
        }
        else
        {
            Debug.LogError("Countdown: TMP_Text reference not set in Inspector.");
            CanStart = true; // fallback so snake can move
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        float count = startCount;

        while (count > 0)
        {
            timerText.text = count.ToString("0"); // Display 3, 2, 1
            timerText.color = Color.yellow;
            yield return new WaitForSeconds(1f);
            count--;
        }

        // Display SSSTART
        timerText.text = "SSSTART";
        timerText.color = Color.green;
        yield return new WaitForSeconds(1f);

        // Hide the timer
        timerText.gameObject.SetActive(false);

        // Signal that the game can start
        CanStart = true;
    }
}
