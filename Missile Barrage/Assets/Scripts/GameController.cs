using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class GameController : MonoBehaviour
{
    public int levelNumber;
    public Slider musicVolume;
    public Slider sfxVolume;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public Button apply;
    public Settings gameSettings;
    public GameObject city;

    private void OnEnable()
    {
        gameSettings = new Settings();
        musicVolume.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        sfxVolume.onValueChanged.AddListener(delegate { OnSFXVolumeChange(); });
        apply.onClick.AddListener(delegate { OnButtonApplyClick(); });

        LoadSettings();
    }

    public void InGamePause()
    {
        Time.timeScale = 0.0f;
    }

    public void InGameResume()
    {
        Time.timeScale = 1.0f;
        GameObject[] rocketColors = GameObject.FindGameObjectsWithTag("Rocket");
        GameObject[] nukeColors = GameObject.FindGameObjectsWithTag("Nuke");
        int i = 0;
        for (; i < rocketColors.Length; i++)
        {
            rocketColors[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        i = 0;
        for (; i < nukeColors.Length; i++)
        {
            nukeColors[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        Color tempColor = new Color(1, 1, 1, 1.0f);
        if (city != null)
            city.GetComponent<Image>().color = tempColor;
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = musicVolume.value;
    }

    public void OnSFXVolumeChange()
    {
        sfxSource.volume = gameSettings.sfxVolume = sfxVolume.value;
        sfxSource.mute = false;
        if (!sfxSource.isPlaying)
            sfxSource.Play();
    }

    public void OnButtonApplyClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Music Volume", musicVolume.value);

        PlayerPrefs.SetFloat("SFX Volume", sfxVolume.value);
    }

    public void LoadSettings()
    {
        musicVolume.value = PlayerPrefs.GetFloat("Music Volume", 1);
        musicSource.volume = musicVolume.value;

        sfxVolume.value = PlayerPrefs.GetFloat("SFX Volume", 1);
        sfxSource.volume = sfxVolume.value;
    }
}
