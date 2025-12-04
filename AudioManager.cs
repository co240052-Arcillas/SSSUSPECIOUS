using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- AUDIO SOURCE ---")]
    [SerializeField] AudioSource musicSource;   // AudioSource for background music

    [Header("--- AUDIO CLIP ---")]
    public AudioClip background; // Background music clip

    private void Start()
    {
        // Play background music when the game starts
        musicSource.clip = background;
        musicSource.Play();
    }
}
