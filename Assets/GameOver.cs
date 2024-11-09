using UnityEngine;
using UnityEngine.SceneManagement; // If you want to restart the scene

public class GameOver : MonoBehaviour
{
    // This function will be called when another collider enters this object's collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "Ball"
        if (other.CompareTag("Ball"))
        {
            // Display Game Over message in the console
            Debug.Log("Game Over!");

            // Optionally, trigger game-over UI or reload the scene
            // You could call a Game Over UI or scene reload here
            // Example of reloading the current scene:
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}