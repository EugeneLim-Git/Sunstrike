using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("UI Audio Clips")]
    [SerializeField] private AudioClip mouseSFX1;
    [SerializeField] private AudioClip mouseSFX2;


    [Header("Audio Sources")]
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        
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
