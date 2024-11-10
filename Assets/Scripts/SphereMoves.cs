
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMoves : MonoBehaviour
{
    public float dropDelay = 2.0f; // Delay in seconds before the ball is dropped
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DropBallAfterDelay()); // Start the coroutine at the start or call it when needed
    }

    private IEnumerator DropBallAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(dropDelay);

        // Enable gravity to drop the ball
        rb.useGravity = true;
    }
}