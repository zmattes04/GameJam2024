using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.UpdateHighScores();
            Debug.Log("Game Over!");
            SceneManager.LoadScene(2);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else if (other.CompareTag("Obstacle")) {
            Destroy(other.gameObject);
        }
    }
}