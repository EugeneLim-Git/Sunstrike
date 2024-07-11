using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class UIVolumeSlider : MonoBehaviour
{
    public AudioMixer uiMixer;
    public Slider uiSlider;
    public void SetLevel(float sliderValue)
    {
        uiMixer.SetFloat("UIVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("UIVolume", sliderValue);
    }

    public void LoadVolume()
    {
        uiSlider.value = PlayerPrefs.GetFloat("UIVolume");
        SetLevel(uiSlider.value);
    }
}
