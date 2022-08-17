using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script responsible for the behaviour of scrollbars that adjusts volume of the game
/// </summary>
[RequireComponent(typeof(Slider))]
public class VolumeScrollbar : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("What type of audio is this scrollbar responsible for?")]
    public AudioSourceAssistant.AudioType audioType;
    [Tooltip("Scrollbar component of this scrollbar")]
    Slider mySlider;

    void Start()
    {
        // Initialize
        mySlider = GetComponent<Slider>();

        try
        {
            // If this scrollbar is responsible for background music
            if (audioType == AudioSourceAssistant.AudioType.BackgroundMusic)
            {
                // Get the background music volume from the audio manager
                mySlider.value = AudioManager.Instance.backgroundVolume;
            }
            // If this scrollbar is responsible for sound effects
            else if (audioType == AudioSourceAssistant.AudioType.SoundFX)
            {
                // Get the sound effects volume from the audio manager
                mySlider.value = AudioManager.Instance.soundFXVolume;
            }
        }
        catch
        {
            Debug.Log("Audio Manager is missing!");
        }
    }
}
