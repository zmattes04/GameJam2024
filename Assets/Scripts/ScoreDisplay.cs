using UnityEngine;
using System;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText; 
    public TMP_Text highScoreText;
    private int[] highScores;
    private float[] highScoreTimes;

    void Start()
    {
        scoreText.text = $"Last Score: {GameManager.score}";;
        highScoreText.text = "High Scores\n";
        GameManager.LoadHighScores();

        for (int i = 0; i < GameManager.highScoresLength; i++)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.highScoreTimes[i]);
            string formattedTime = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
            highScoreText.text += $"{GameManager.highScores[i]}     {formattedTime}\n";
        }
    }
}
