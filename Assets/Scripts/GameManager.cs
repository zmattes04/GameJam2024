using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject board;
    public List<GameObject> intrusions;
    public List<GameObject> extrusions;
    public Vector3 boardScale;
    public int holeCountCenter;
    public int holeCountEdges;
    public int rampCountCenter;
    public int rampCountEdges;
    public static int highScoresLength = 10;
    public static int[] highScores = new int[highScoresLength];
    public static float[] highScoreTimes = new float[highScoresLength];
    public static string[] highScoreNames = new string[highScoresLength];
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
    public static string username;
    private static DynamicDifficulty dynamicDifficulty;

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

        // Load Settings
        dynamicDifficulty = GetComponent<DynamicDifficulty>();
        board.GetComponent<BoardTilt>().verticalRotationSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 200f);
        board.GetComponent<BoardTilt>().horizontalRotationSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 200f);      
        soundEffectSource.volume = PlayerPrefs.GetFloat("GameSFXVolume", 0.3f);
        gameMusicSource.volume = PlayerPrefs.GetFloat("GameMusicVolume", 0.3f);

        GameObject boardMesh = board.transform.GetChild(0).gameObject;

        // Add center holes
        Debug.Log("center holes: " + PlayerPrefs.GetInt("Difficulty", 1));
        for (int i = 0; i < PlayerPrefs.GetInt("Difficulty", 1); i++)
        {
            int intrusionsIndex = UnityEngine.Random.Range(0, intrusions.Count);
            boardMesh.GetComponent<GenerateHoles>().PerformSubtraction(boardMesh, intrusions[intrusionsIndex], boardScale, minX_CenterHoles, maxX_CenterHoles, minZ_CenterHoles, maxZ_CenterHoles);
        }

        // Determine where the edge holes will go randomly and add
        for (int i = 0; i < holeCountEdges; i++)
        {
            int intrusionsIndex = UnityEngine.Random.Range(0, intrusions.Count);
            float randomValue = UnityEngine.Random.value;
            if (randomValue < 0.25f)
            {
                boardMesh.GetComponent<GenerateHoles>().PerformSubtraction(boardMesh, intrusions[intrusionsIndex], boardScale, minX, minX_EdgeHoles, minZ, maxZ);
            }
            else if (randomValue < 0.5f)
            {
                boardMesh.GetComponent<GenerateHoles>().PerformSubtraction(boardMesh, intrusions[intrusionsIndex], boardScale, maxX_EdgeHoles, maxX, minZ, maxZ);
            }
            else if (randomValue < 0.5f)
            {
                boardMesh.GetComponent<GenerateHoles>().PerformSubtraction(boardMesh, intrusions[intrusionsIndex], boardScale, minX, maxX, minZ, minZ_EdgeHoles);
            }
            else
            {
                boardMesh.GetComponent<GenerateHoles>().PerformSubtraction(boardMesh, intrusions[intrusionsIndex], boardScale, minX, maxX, maxZ_EdgeHoles, maxZ);
            }
        }

        for (int i = 0; i < rampCountCenter; i++)
        {
            int extrustionIndex = UnityEngine.Random.Range(0, extrusions.Count);
            boardMesh.GetComponent<GenerateHoles>().PerformAddition(boardMesh, extrusions[extrustionIndex], boardScale, minX_CenterHoles, maxX_CenterHoles, minZ_CenterHoles, maxZ_CenterHoles);
        }


        for (int i = 0; i < rampCountEdges; i++)
        {
            float randomValue = UnityEngine.Random.value;
            int extrustionIndex = UnityEngine.Random.Range(0, extrusions.Count);
            if (randomValue < 0.25f)
            {
                boardMesh.GetComponent<GenerateHoles>().PerformAddition(boardMesh, extrusions[extrustionIndex], boardScale, minX, minX_EdgeHoles, minZ, maxZ);
            }
            else if (randomValue < 0.5f)
            {
                boardMesh.GetComponent<GenerateHoles>().PerformAddition(boardMesh, extrusions[extrustionIndex], boardScale, maxX_EdgeHoles, maxX, minZ, maxZ);
            }
            else if (randomValue < 0.5f)
            {
                boardMesh.GetComponent<GenerateHoles>().PerformAddition(boardMesh, extrusions[extrustionIndex], boardScale, minX, maxX, minZ, minZ_EdgeHoles);
            }
            else
            {
                boardMesh.GetComponent<GenerateHoles>().PerformAddition(boardMesh, extrusions[extrustionIndex], boardScale, minX, maxX, maxZ_EdgeHoles, maxZ);
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
        GameManager.dynamicDifficulty.AddScore(score);
        if (score > highScores[highScoresLength - 1])
        {
            highScores[highScoresLength - 1] = score;
            highScoreTimes[highScoresLength - 1] = gameTimer;
            highScoreNames[highScoresLength - 1] = username;

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

                        string tempName = highScoreNames[i];
                        highScoreNames[i] = highScoreNames[j];
                        highScoreNames[j] = tempName;
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
            PlayerPrefs.SetString("HighScoreName" + i, highScoreNames[i]);
        }
        PlayerPrefs.Save();
    }

    public static void LoadHighScores()
    {
        for (int i = 0; i < highScoresLength; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
            highScoreTimes[i] = PlayerPrefs.GetFloat("HighScoreTime" + i, 0);
            highScoreNames[i] = PlayerPrefs.GetString("HighScoreName" + i, "");
        }
        highScore = highScores[0];
    }
}

