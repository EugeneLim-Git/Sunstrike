using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("UI Audio Clips")]
    [SerializeField] private AudioClip mouseSFX1;
    [SerializeField] private AudioClip mouseSFX2;


    [Header("Audio Sources")]
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [Header("Audio Sliders")]
    [SerializeField] private MasterVolumeSlider masterSlider;
    [SerializeField] private UIVolumeSlider uiSlider;
    [SerializeField] private SFXVolumeSlider sfxSlider;
    [SerializeField] private SFXVolumeSlider musicSlider;



    // Start is called before the first frame update
    public void Initialise()
    {
        masterSlider.LoadVolume();
        sfxSlider.LoadVolume();
        uiSlider.LoadVolume();
        musicSlider.LoadVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uiAudioSource.PlayOneShot(mouseSFX1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            uiAudioSource.PlayOneShot(mouseSFX2);
        }
    }
}
