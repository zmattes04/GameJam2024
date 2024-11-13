using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour
{
    public AudioSource menuMusic;

    void Start()
    {
        menuMusic.volume = PlayerPrefs.GetFloat("MenuMusicVolume", 0.3f);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
