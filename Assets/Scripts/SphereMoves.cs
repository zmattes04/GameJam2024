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
                ResetBallPosition();
            }
        }
    }

    void ResetBallPosition()
    {
        rb.MovePosition(new Vector3(transform.position.x, boardTransform.position.y + 0.5f + teleportDistance, transform.position.z));
        rb.velocity = Vector3.zero;
    }
}