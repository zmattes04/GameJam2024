using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Coin : MonoBehaviour
{
    public float xMin = -10f;
    public float xMax = 10f;
    public float zMin = -10f;
    public float zMax = 10f;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public SoundEffectPlayer soundEffectPlayer;
    public SoundEffectType soundEffectType;
    public GenerateHoles generateHolesScript;
    public float minDistanceFromHole = 1.5f;

    void Start()
    {
        GameManager.UpdateHighScore(GameManager.highScore, highScoreText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.score++;
            if (GameManager.score > GameManager.highScore)
            {
                GameManager.UpdateHighScore(GameManager.score, highScoreText);
                PlayerPrefs.SetInt("HighScore", GameManager.score);
                PlayerPrefs.Save();
            }
            Debug.Log("Score: " + GameManager.score);

            Vector3 newPosition;
            do
            {
                float randomX = Random.Range(xMin, xMax);
                float randomZ = Random.Range(zMin, zMax);
                newPosition = new Vector3(randomX, transform.position.y, randomZ);
            } while (!IsSafePosition(newPosition));

            transform.position = newPosition;
            scoreText.text = "Score: " + GameManager.score;
            soundEffectPlayer.PlaySoundEffect(soundEffectType);
        }
        PlayerPrefs.SetInt("Score", GameManager.score);
        PlayerPrefs.Save();
    }


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
}
