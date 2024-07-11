using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour
{
    public AudioMixer sfxMixer;
    public Slider sfxSlider;
    
    public void SetLevel(float sliderValue)
    {
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void LoadVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetLevel(sfxSlider.value);
    }
}
