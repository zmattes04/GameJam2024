using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Coin : MonoBehaviour
{
    public int score = 0;
    public float xMin = -10f;
    public float xMax = 10f;
    public float zMin = -10f;
    public float zMax = 10f;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public SoundEffectPlayer soundEffectPlayer;
    public SoundEffectType soundEffectType;
    public GenerateHoles generateHolesScript;  // Reference to GenerateHoles script
    public float minDistanceFromHole = 1.5f;   // Minimum safe distance from holes

    void Start()
    {
        GameManager.UpdateHighScore(GameManager.highScore, highScoreText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            score++;
            if (score > GameManager.highScore)
            {
                GameManager.UpdateHighScore(score, highScoreText);
                PlayerPrefs.SetInt("HighScore", score);
                PlayerPrefs.Save();
            }
            Debug.Log("Score: " + score);

            Vector3 newPosition;
            do
            {

                float randomX = Random.Range(xMin, xMax);
                float randomZ = Random.Range(zMin, zMax);
                newPosition = new Vector3(randomX, transform.position.y, randomZ);
            } while (!IsSafePosition(newPosition));

            transform.position = newPosition;
            scoreText.text = "Score: " + score;
            soundEffectPlayer.PlaySoundEffect(soundEffectType);
        }
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    // Check if the generated position is at a safe distance from all holes
    private bool IsSafePosition(Vector3 position)
    {
        List<Vector3> holePositions = generateHolesScript.getHolePositions();
        foreach (Vector3 holePosition in holePositions)
        {
            float distance = Vector3.Distance(position, holePosition);
            if (distance < minDistanceFromHole)
            {
                return false;
            }
        }
        return true;
    }
    public int getscore()
    {
        return score;
    }
}
