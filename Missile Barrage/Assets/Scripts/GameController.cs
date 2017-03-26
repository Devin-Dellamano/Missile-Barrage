using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class GameController : MonoBehaviour
{
    public int levelNumber;
    public Slider musicVolume;
    public Slider secondMusicVolume;
    public Slider sfxVolume;
    public Slider secondSFXVolume;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public Button apply;
    public Settings gameSettings;

    private void OnEnable()
    {
        gameSettings = new Settings();
        musicVolume.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        if (secondMusicVolume != null)
            secondMusicVolume.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });

        sfxVolume.onValueChanged.AddListener(delegate { OnSFXVolumeChange(); });
        if (secondSFXVolume != null)
            secondSFXVolume.onValueChanged.AddListener(delegate { OnSFXVolumeChange(); });
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
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = musicVolume.value;
        if (secondMusicVolume != null)
            musicSource.volume = gameSettings.musicVolume = secondMusicVolume.value;
    }

    public void OnSFXVolumeChange()
    {
        sfxSource.volume = gameSettings.sfxVolume = sfxVolume.value;
        sfxSource.mute = false;
        if (!sfxSource.isPlaying)
            sfxSource.Play();

        if (secondSFXVolume != null)
        {
            sfxSource.volume = gameSettings.sfxVolume = secondSFXVolume.value;
            sfxSource.mute = false;
            if (!sfxSource.isPlaying)
                sfxSource.Play();
        }
    }

    public void OnButtonApplyClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Music Volume", musicVolume.value);
        if (secondMusicVolume != null)
            PlayerPrefs.SetFloat("Music Volume", secondMusicVolume.value);

        PlayerPrefs.SetFloat("SFX Volume", sfxVolume.value);
        if (secondSFXVolume != null)
            PlayerPrefs.SetFloat("SFX Volume", secondSFXVolume.value);


        //string data = JsonUtility.ToJson(gameSettings, true);

        //File.WriteAllText(Application.persistentDataPath + "/volume.json", data);
    }

    public void LoadSettings()
    {
        musicVolume.value = PlayerPrefs.GetFloat("Music Volume", 1);
        musicSource.volume = musicVolume.value;
        if (secondMusicVolume != null)
        {
            secondMusicVolume.value = PlayerPrefs.GetFloat("Music Volume", 1);
            musicSource.volume = secondMusicVolume.value;
        }

        sfxVolume.value = PlayerPrefs.GetFloat("SFX Volume", 1);
        sfxSource.volume = sfxVolume.value;
        if (secondSFXVolume != null)
        {
            secondSFXVolume.value = PlayerPrefs.GetFloat("SFX Volume", 1);
            sfxSource.volume = secondSFXVolume.value;
        }
    }
}
