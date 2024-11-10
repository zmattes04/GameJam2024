using UnityEngine;
using TMPro;

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
            }
            Debug.Log("Score: " + score);

            float randomX = Random.Range(xMin, xMax);
            float randomZ = Random.Range(zMin, zMax);

            transform.position = new Vector3(randomX, transform.position.y, randomZ);
            scoreText.text = "Score: " + score;
            soundEffectPlayer.PlaySoundEffect(soundEffectType);
        }
    }
}
