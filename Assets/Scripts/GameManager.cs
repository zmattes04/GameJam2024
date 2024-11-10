using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject board;
    public GameObject hole;
    public Vector3 boardScale;
    public int holeCount;
    public static int highScore = 0;

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

        for (int i = 0; i < holeCount; i++)
        {
            board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale);
        }
    }

    public static void UpdateHighScore(int score, TMP_Text highScoreText)
    {
        highScore = score;
        highScoreText.text = "High Score: " + highScore;
    }
}

