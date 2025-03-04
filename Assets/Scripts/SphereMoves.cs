using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMoves : MonoBehaviour
{
    public float dropDelay = 2.0f;
    private Rigidbody rb;
    public GameObject Board;
    [SerializeField] private Transform boardTransform;
    [SerializeField] private float raycastDistance = 1.0f;
    [SerializeField] private float teleportDistance = 1.0f;

    public float highYIncrement;
    public float heightAboveBoard;
    public string boardTag = "Board";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DropBallAfterDelay());
        Board = GameObject.FindWithTag("Board");
        boardTransform = Board.transform;
    }


    private IEnumerator DropBallAfterDelay()
    {
        yield return new WaitForSeconds(dropDelay);
        rb.useGravity = true;
    }

  
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, raycastDistance))
        {
            if (hit.transform == boardTransform)
            {
                //ResetBallPosition();
                AdjustHeightToBoard();
            }
        }
    }

    private void ResetBallPosition()
    {
        rb.MovePosition(new Vector3(transform.position.x, boardTransform.position.y + 0.5f + teleportDistance, transform.position.z));
        rb.velocity = Vector3.zero;
    }

    private void AdjustHeightToBoard()
    {
        RaycastHit hit;
        // Cast a ray downward from a high point to detect the board
        if (Physics.Raycast(transform.position + Vector3.up * highYIncrement, Vector3.down, out hit, Mathf.Infinity))
        {
            // Set the Y position to be just above the board surface
            if (hit.collider.CompareTag(boardTag))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + heightAboveBoard, transform.position.z);
                rb.velocity = Vector3.zero;
            }
        }
    }
}