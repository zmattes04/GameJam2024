using UnityEngine;
using System;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText; 
    public TMP_Text highScoreText;
    private int[] highScores;
    private float[] highScoreTimes;

    public TMP_Text scoreAdjustedText;
    public TMP_Text highScoreAdjustedText;
    private float[] highScoresAdjusted;
    private float[] highScoreAdjustedTimes;

    void Start()
    {
        scoreText.text = $"Last Score: {GameManager.score}";;
        highScoreText.text = "High Scores\n";
        scoreAdjustedText.text = $"Last Score W/ Mulitiplier: {GameManager.scoreAdjusted}"; ;
        highScoreAdjustedText.text = "High Scores W/ Multiplier\n";
        GameManager.LoadHighScores();

        for (int i = 0; i < GameManager.highScoresLength; i++)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.highScoreTimes[i]);
            string formattedTime = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
            highScoreText.text += $"{GameManager.highScoreNames[i]}: {GameManager.highScores[i]}     {formattedTime}\n";
            TimeSpan timeSpanAdjusted = TimeSpan.FromSeconds(GameManager.highScoreAdjustedTimes[i]);
            string formattedTimeAdjusted = $"{timeSpanAdjusted.Minutes}:{timeSpanAdjusted.Seconds:D2}";
            highScoreAdjustedText.text += $"{GameManager.highScoreAdjustedNames[i]}: {GameManager.highScoresAdjusted[i]}     {formattedTimeAdjusted}\n";
        }
    }
}
