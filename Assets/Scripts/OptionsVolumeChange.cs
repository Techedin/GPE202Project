using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsVolumeChange : MonoBehaviour
{
    public AudioMixer mainAudioMixer;
    public Slider musicVolumeSlider;
    public Slider spxVolumeSlider;

    public void Start()
    {
        OnMusicVolumeChange();
        OnSPXVolumeChange();
    }

    public void OnMusicVolumeChange()
    {
        // Start with the slider value
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
        {
            // Set lowest slider value to lowest volume
            newVolume = -80;
        }
        else
        {
            // Log Math Stuff
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("MusicVolume", newVolume);
    }
    public void OnSPXVolumeChange()
    {
        // Start with the slider value
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
        {
            // Set lowest slider value to lowest volume
            newVolume = -80;
        }
        else
        {
            // Log Math Stuff
            newVolume = Mathf.Log10(newVolume);
            // Make it in the 0-20db range
            newVolume = newVolume * 20;
        }

        // Set the volume to the new volume setting
        mainAudioMixer.SetFloat("SFXVolume", newVolume);
    }
}
