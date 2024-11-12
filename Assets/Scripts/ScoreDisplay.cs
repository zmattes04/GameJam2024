using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText; 
    public TMP_Text highScoreText;
    private int[] highScores;

    void Start()
    {
        scoreText.text = $"Last Score: {GameManager.score}";;
        highScoreText.text = "High Scores\n";

        for (int i = 0; i < GameManager.highScoresLength; i++)
        {
            highScoreText.text += $"{GameManager.highScores[i]}\n";
        }
    }
}
