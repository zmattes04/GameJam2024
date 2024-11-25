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


    void Start()
    {
        gameMusicVolumeSlider.value = MainMenu.gameMusicVolume;
        gameSFXVolumeSlider.value = MainMenu.gameSFXVolume;
        mouseSensitivitySlider.value = MainMenu.mouseSensitivity;
        menuMusicVolumeSlider.value = MainMenu.menuMusicVolume;
    }
    
    public void UpdateMenuMusicVolume()
    {
        MainMenu.menuMusicVolume = menuMusicVolumeSlider.value;
        menuMusic.volume = MainMenu.menuMusicVolume;
        PlayerPrefs.SetFloat("MenuMusicVolume", menuMusicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateGameMusicVolume()
    {
        MainMenu.gameMusicVolume = gameMusicVolumeSlider.value;
        PlayerPrefs.SetFloat("GameMusicVolume", gameMusicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateGameSFXVolume()
    {
        MainMenu.gameSFXVolume = gameSFXVolumeSlider.value;
        PlayerPrefs.SetFloat("GameSFXVolume", gameSFXVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateMouseSensitivity()
    {
        MainMenu.mouseSensitivity = mouseSensitivitySlider.value;
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivitySlider.value);
        PlayerPrefs.Save();
    }
}
