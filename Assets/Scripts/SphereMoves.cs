using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMoves : MonoBehaviour
{
    public Vector3 startTargetPosition;
    public float startMoveDuration = 2f;
    public float startOvershootAmount = 1.5f;
    public float startOscillationDamping = 0.1f;

    private Vector3 startVelocity = Vector3.zero;
    private float startElapsedTime = 0f;
    private bool isStarting = false;
    public float delayBeforeStart = 1f;

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
        StartCoroutine(StartMovementAfterDelay()); // Start coroutine to wait before moving
    }

    private IEnumerator StartMovementAfterDelay()
    {
        // Wait for the specified delay before starting the movement
        yield return new WaitForSeconds(delayBeforeStart);

        // Begin moving the object
        isStarting = true;
    }

    void Update()
    {
        // Oscillating effect to start the game
        if (isStarting)
        {
            startElapsedTime += Time.deltaTime;

            // Calculate the fraction of time elapsed (0 to 1)
            float t = Mathf.Clamp01(startElapsedTime / startMoveDuration);

            // Calculate the overshoot position based on current progress (fraction of the total duration)
            Vector3 direction = startTargetPosition - transform.position;
            Vector3 overshotPosition = startTargetPosition + direction.normalized * startOvershootAmount;

            // Move the object smoothly towards the overshot position using smooth damp
            transform.position = Vector3.SmoothDamp(transform.position, overshotPosition, ref startVelocity, 1f);

            // Apply oscillation back towards the target with decreasing overshoot as time progresses
            startOvershootAmount *= 1 - startOscillationDamping * t;
        }
    }


    private IEnumerator DropBallAfterDelay()
    {
        yield return new WaitForSeconds(dropDelay);
        transform.position = startTargetPosition;
        isStarting = false;
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