using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ball"))
        {
            
            Debug.Log("Game Over!");

            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}