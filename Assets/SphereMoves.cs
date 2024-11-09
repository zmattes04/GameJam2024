
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMoves : MonoBehaviour
{
    public Transform boardTransform; // Reference to the board's transform
    public float movementSpeed = 5f; // Speed of the sphere's movement

    public Rigidbody rb;

    private void Start()
    {
        // Make sure the sphere has a Rigidbody component for physics-based movement
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get the tilt of the board
        Vector3 boardTilt = boardTransform.rotation.eulerAngles;

        // Convert tilt to movement direction (taking into account Unity's angle wrapping)
        float tiltX = (boardTilt.x > 180) ? boardTilt.x - 360 : boardTilt.x;
        float tiltZ = (boardTilt.z > 180) ? boardTilt.z - 360 : boardTilt.z;

        // Calculate the direction based on the board's tilt
        Vector3 movementDirection = new Vector3(-tiltZ, 0, tiltX).normalized;

        // Move the sphere in the calculated direction

        rb.AddForce(movementDirection * movementSpeed, ForceMode.VelocityChange);
 
    }
}