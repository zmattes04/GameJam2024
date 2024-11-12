using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public static float menuMusicVolume;
    public static float gameMusicVolume;
    public static float gameSFXVolume;
    public static float mouseSensitivity;
    public AudioSource menuMusic;

    void Start()
    {
        gameMusicVolume = PlayerPrefs.GetFloat("GameMusicVolume", 0.2f);
        gameSFXVolume = PlayerPrefs.GetFloat("GameSFXVolume", 0.2f);
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 200f);
        menuMusicVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 0.3f);
        menuMusic.volume = menuMusicVolume;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
