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
    public int scoreIncrement;

    private Vector3 newPosition;
    [SerializeField] private List<GameObject> particlePrefabs;
    [SerializeField] int particleDivider;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private float R, G, B;
    private Color currentColor = new Color(1f, 1f, 0f);
    [SerializeField] float colorIncrement;

    public CameraShake cameraShake;

    void Start()
    {
        GameManager.UpdateHighScore(GameManager.highScore, highScoreText);
        currentColor = new Color(R, G, B);
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void Update()
    {
        if (musicSource != null)
        {
            // Scale pitch with score: Higher score -> Faster music
            musicSource.pitch = Mathf.Clamp(1.0f + (GameManager.score / 1000f), 1.0f, 2.0f);
        }
        else
        {
            Debug.LogWarning("Music AudioSource is not assigned!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.score += scoreIncrement + GameManager.dynamicDifficulty.scoreMultipliers[PlayerPrefs.GetInt("Difficulty", 1) - 1];
            SpawnParticles();
            cameraShake.Shake(0.5f, 0.3f);
            if (GameManager.score > GameManager.highScore)
            {
                GameManager.UpdateHighScore(GameManager.score, highScoreText);
                PlayerPrefs.SetInt("HighScore", GameManager.score);
                PlayerPrefs.Save();
            }

            do
            {
                float randomX = Random.Range(xMin, xMax);
                float randomZ = Random.Range(zMin, zMax);
                newPosition = new Vector3(randomX, transform.position.y + highYIncrement, randomZ);
            } while (!AdjustHeightToBoard() || !IsSafePosition(newPosition));
             
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
                return true;
            }
        }
        return false;
    }

    private void SpawnParticles()
    {
        if (particlePrefabs.Count == 0)
        {
            Debug.LogWarning("No particle prefabs assigned!");
            return;
        }
        // Update the current color
        UpdateColorBasedOnScore();

        // Updata all 4 particles colors
        for (int i = 0; i < particlePrefabs.Count; i++)
        {
            ParticleSystemRenderer psRenderer = particlePrefabs[i].GetComponent<ParticleSystemRenderer>();

            // Change particles color
            if (psRenderer != null && psRenderer.sharedMaterial != null)
            {
                psRenderer.sharedMaterial.SetColor("_Color", currentColor);
                psRenderer.sharedMaterial.SetColor("_EmissionColor", currentColor);
                Debug.Log(currentColor);
            }
            else
            {
                Debug.LogWarning("No material found on ParticleSystemRenderer.");
            }
        }

        // Change the coin color
        Renderer objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null && objectRenderer.material != null)
        {
            objectRenderer.material.SetColor("_Color", currentColor);
        }
        else
        {
            Debug.LogWarning("No material found on GameObject Renderer.");
        }

        // Spawn random particle effect
        int index = Random.Range(0, particlePrefabs.Count - 1);
        GameObject spawnedParticle =  Instantiate(particlePrefabs[index], transform.position, Quaternion.identity);
    }

    private void UpdateColorBasedOnScore()
    {
        if (G > 0.0f)
        {
            // Decrease G by increment until G reaches 0
            G = Mathf.Max(0f, G - colorIncrement);
        } else
        {
            // Increase B by increment until B reaches 1
            B = Mathf.Min(1f, B + colorIncrement);
        }
        currentColor = new Color(R, G, B);
    }
}
