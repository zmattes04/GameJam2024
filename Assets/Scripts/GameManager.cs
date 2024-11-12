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
    public static int highScore;
    public static int score;
    public float minX_CenterHoles, maxX_CenterHoles, minZ_CenterHoles, maxZ_CenterHoles;
    public float minX_EdgeHoles, maxX_EdgeHoles, minZ_EdgeHoles, maxZ_EdgeHoles;

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

        for (int i = 0; i < holeCountCenter; i++)
        {
            board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale, minX_CenterHoles, maxX_CenterHoles, minZ_CenterHoles, maxZ_CenterHoles);
        }

        for (int i = 0; i < holeCountEdges; i++)
        {
            board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale, minX_EdgeHoles, maxX_EdgeHoles, minZ_EdgeHoles, maxZ_EdgeHoles);
        }
        score = 0;
    }

    public static void UpdateHighScore(int score, TMP_Text highScoreText)
    {
        highScore = score;
        highScoreText.text = "High Score: " + highScore;
    }

    public static void UpdateHighScores()
    {
        if (score > highScores[highScoresLength - 1])
        {
            highScores[highScoresLength - 1] = score;
            Array.Sort(highScores);
            Array.Reverse(highScores);
        }

        highScore = highScores[0];
    }
}

