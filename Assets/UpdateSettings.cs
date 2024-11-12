using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSettings : MonoBehaviour
{
    public Slider menuMusicVolumeSlider;
    public Slider gameMusicVolumeSlider;
    public Slider gameSFXVolumeSlider;
    public Slider mouseSensitivitySlider;
    public AudioSource menuMusic;
    public static float menuMusicVolume;
    public static float gameMusicVolume;
    public static float gameSFXVolume;
    public static float mouseSensitivity;


    void Start()
    {
        gameMusicVolume = PlayerPrefs.GetFloat("GameMusicVolume", 0.2f);
        gameSFXVolume = PlayerPrefs.GetFloat("GameSFXVolume", 0.2f);
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 200f);
        menuMusicVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 0.3f);

        gameMusicVolumeSlider.value = gameMusicVolume;
        gameSFXVolumeSlider.value = gameSFXVolume;
        mouseSensitivitySlider.value = mouseSensitivity;
        menuMusicVolumeSlider.value = menuMusicVolume;

        menuMusic.volume = menuMusicVolume;
    }
    
    public void UpdateMenuMusicVolume()
    {
        menuMusicVolume = menuMusicVolumeSlider.value;
        menuMusic.volume = menuMusicVolume;
        PlayerPrefs.SetFloat("MenuMusicVolume", menuMusicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateGameMusicVolume()
    {
        gameMusicVolume = gameMusicVolumeSlider.value;
        PlayerPrefs.SetFloat("GameMusicVolume", gameMusicVolume);
        PlayerPrefs.Save();
    }

    public void UpdateGameSFXVolume()
    {
        gameSFXVolume = gameSFXVolumeSlider.value;
        PlayerPrefs.SetFloat("GameSFXVolume", gameSFXVolume);
        PlayerPrefs.Save();
    }

    public void UpdateMouseSensitivity()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.Save();
    }
}
