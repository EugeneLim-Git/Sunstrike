using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseMenu;

    [Header("UI Audio Clips")]
    [SerializeField] private AudioClip mouseSFX1;
    [SerializeField] private AudioClip mouseSFX2;

    [Header("Battle Audio Clips")]
    [SerializeField] private AudioClip damageSFXClip;
    [SerializeField] private AudioClip healSFXClip;
    [SerializeField] private AudioClip buffSFXClip;
    [SerializeField] private AudioClip debuffSFXClip;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [Header("Audio Sliders")]
    [SerializeField] private MasterVolumeSlider masterSlider;
    [SerializeField] private UIVolumeSlider uiSlider;
    [SerializeField] private SFXVolumeSlider sfxSlider;
    [SerializeField] private MusicVolumeSlider musicSlider;



    // Start is called before the first frame update
    public void Initialise()
    {
        masterSlider.LoadVolume();
        sfxSlider.LoadVolume();
        uiSlider.LoadVolume();
        musicSlider.LoadVolume();

        pauseMenu.SetActive(false);
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

    public void PlaySkillSFX(BaseSkill.SkillType skillType)
    {
        if (skillType == BaseSkill.SkillType.Damage)
        {
            sfxAudioSource.clip = damageSFXClip;
            sfxAudioSource.Play();
        }
        else if (skillType == BaseSkill.SkillType.Buff)
        {
            sfxAudioSource.PlayOneShot(buffSFXClip);
        }
        else if (skillType == BaseSkill.SkillType.Debuff)
        {
            sfxAudioSource.PlayOneShot(debuffSFXClip);
        }
        else if (skillType == BaseSkill.SkillType.Heal)
        {
            sfxAudioSource.PlayOneShot(healSFXClip);
        }
    }

}
