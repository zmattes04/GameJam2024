using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public static float menuMusicVolume;
    public static float gameMusicVolume;
    public static float gameSFXVolume;
    public static float mouseSensitivity;
    public AudioSource menuMusic;
    public TMP_InputField usernameText;

    void Awake()
    {
        usernameText.text = PlayerPrefs.GetString("Username", "Enter username");
    }

    void Start()
    {
        gameMusicVolume = PlayerPrefs.GetFloat("GameMusicVolume", 0.2f);
        gameSFXVolume = PlayerPrefs.GetFloat("GameSFXVolume", 0.2f);
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 200f);
        menuMusicVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 0.3f);
        GameManager.username = PlayerPrefs.GetString("Username", "Username");
        menuMusic.volume = menuMusicVolume;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log(GameManager.username);
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
