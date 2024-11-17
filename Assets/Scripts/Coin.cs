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

    public float highYIncrement;
    public float heightAboveBoard;
    public string boardTag = "Board";

    private Vector3 newPosition;

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

            do
            {
                float randomX = Random.Range(xMin, xMax);
                float randomZ = Random.Range(zMin, zMax);
                newPosition = new Vector3(randomX, transform.position.y + highYIncrement, randomZ);
            } while (!AdjustHeightToBoard());
             
            transform.position = newPosition;
            scoreText.text = "Score: " + GameManager.score;
            soundEffectPlayer.PlaySoundEffect(soundEffectType);
        }
        PlayerPrefs.SetInt("Score", GameManager.score);
        PlayerPrefs.Save();
    }


    private bool AdjustHeightToBoard()
    {
        RaycastHit hit;
        // Cast a ray downward from a high point to detect the board
        if (Physics.Raycast(newPosition + Vector3.up * highYIncrement, Vector3.down, out hit, Mathf.Infinity))
        {
            // Set the Y position to be just above the board surface
            if (hit.collider.CompareTag(boardTag))
            {
                newPosition.y = hit.point.y + heightAboveBoard;
                Debug.Log("Hit");
                return true;
            }
        }
        Debug.Log("Missed.");
        return false;
    }
}
