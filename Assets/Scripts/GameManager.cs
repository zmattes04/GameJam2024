using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject board;
    public GameObject hole;
    public Vector3 boardScale;
    public int holeCountCenter;
    public int holeCountEdges;
    public static int highScoresLength = 10;
    public static int[] highScores = new int[highScoresLength];
    public static float[] highScoreTimes = new float[highScoresLength];
    public static int highScore;
    public static int score;
    public float minX_CenterHoles, maxX_CenterHoles, minZ_CenterHoles, maxZ_CenterHoles;
    public float minX_EdgeHoles, maxX_EdgeHoles, minZ_EdgeHoles, maxZ_EdgeHoles;
    public float minX, maxX, minZ, maxZ;
    public AudioSource soundEffectSource;
    public AudioSource gameMusicSource;
    public static float gameTimer = 0f;
    public TMP_Text timerText;
    private static bool gameOver;

    void awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        board.GetComponent<BoardTilt>().verticalRotationSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 200f);
        board.GetComponent<BoardTilt>().horizontalRotationSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 200f);      
        soundEffectSource.volume = PlayerPrefs.GetFloat("GameSFXVolume", 0.3f);
        gameMusicSource.volume = PlayerPrefs.GetFloat("GameMusicVolume", 0.3f); ;

        for (int i = 0; i < holeCountCenter; i++)
        {
            board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale, minX_CenterHoles, maxX_CenterHoles, minZ_CenterHoles, maxZ_CenterHoles);
        }

        for (int i = 0; i < holeCountEdges; i++)
        {
            float randomValue = UnityEngine.Random.value;
            if (randomValue < 0.25f)
            {
                board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale, minX, minX_EdgeHoles, minZ, maxZ);
            }
            else if (randomValue < 0.5f)
            {
                board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale, maxX_EdgeHoles, maxX, minZ, maxZ);
            }
            else if (randomValue < 0.5f)
            {
                board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale, minX, maxX, minZ, minZ_EdgeHoles);
            }
            else
            {
                board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale, minX, maxX, maxZ_EdgeHoles, maxZ);
            }
        }
        gameOver = false;
        score = 0;
        gameTimer = 0f;
    }

    void Update()
    {
        if (!gameOver)
        {
            gameTimer += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTimer);
            timerText.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
        }
    }

    public static void UpdateHighScore(int score, TMP_Text highScoreText)
    {
        highScore = score;
        highScoreText.text = "High Score: " + highScore;
    }

    public static void UpdateHighScores()
    {
        gameOver = true;
        if (score > highScores[highScoresLength - 1])
        {
            highScores[highScoresLength - 1] = score;
            highScoreTimes[highScoresLength - 1] = gameTimer;

            for (int i = 0; i < highScoresLength; i++)
            {
                for (int j = i + 1; j < highScoresLength; j++)
                {
                    if (highScores[i] < highScores[j])
                    {
                        // Swap high scores
                        int tempScore = highScores[i];
                        highScores[i] = highScores[j];
                        highScores[j] = tempScore;

                        // Swap associated times
                        float tempTime = highScoreTimes[i];
                        highScoreTimes[i] = highScoreTimes[j];
                        highScoreTimes[j] = tempTime;
                    }
                }
            }
        }
        highScore = highScores[0];
        SaveHighScores();
    }

    public static void SaveHighScores()
    {
        for (int i = 0; i < highScoresLength; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
            PlayerPrefs.SetFloat("HighScoreTime" + i, highScoreTimes[i]);
        }
        PlayerPrefs.Save();
    }

    public static void LoadHighScores()
    {
        for (int i = 0; i < highScoresLength; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
            highScoreTimes[i] = PlayerPrefs.GetFloat("HighScoreTime" + i, 0);
        }
        highScore = highScores[0];
    }
}

