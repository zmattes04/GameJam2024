using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
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