
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMoves : MonoBehaviour
{
    public Transform boardTransform; // Reference to the board
    public float movementSpeed = 5f; // Speed

    public Rigidbody rb;

    private void Start()
    {
        //Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 boardTilt = boardTransform.rotation.eulerAngles;

        
        float tiltX = (boardTilt.x > 180) ? boardTilt.x - 360 : boardTilt.x;
        float tiltZ = (boardTilt.z > 180) ? boardTilt.z - 360 : boardTilt.z;

        
        Vector3 movementDirection = new Vector3(-tiltZ, 0, tiltX).normalized;

        

        rb.AddForce(movementDirection * movementSpeed, ForceMode.VelocityChange);
 
    }
}