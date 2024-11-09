using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ball"))
        {
            
            Debug.Log("Game Over!");

            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}