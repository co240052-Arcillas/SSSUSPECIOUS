using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- AUDIO SOURCE---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--- AUDIO CLIP---")]
    public AudioClip background;
    public AudioClip buttons;

    private void Start()
    {
        // Sets the background music clip and starts playing it at the beginning
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        // Plays a sound effect clip once without interrupting background music
        SFXSource.PlayOneShot(clip);
    }
}
