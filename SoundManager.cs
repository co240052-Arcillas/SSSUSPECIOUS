using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        // Initializes the volume slider with saved value or default and applies it
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        // Updates the global audio volume and saves the setting
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        // Loads the saved music volume and applies it to the slider
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        // Saves the current slider value as the music volume
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
