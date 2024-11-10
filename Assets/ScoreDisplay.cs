using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText; 
    public TMP_Text highScoreText; 
    void Start()
    {
        
        int score = PlayerPrefs.GetInt("Score", 0); 
        int highScore = PlayerPrefs.GetInt("HighScore", 0); 

        // Display the scores in the UI
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
