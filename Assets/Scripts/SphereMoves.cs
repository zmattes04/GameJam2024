
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMoves : MonoBehaviour
{
    public float dropDelay = 2.0f;
    private Rigidbody rb;
    public GameObject Board;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DropBallAfterDelay());
    }

    private IEnumerator DropBallAfterDelay()
    {
        yield return new WaitForSeconds(dropDelay);
        while (Board.transform.rotation == Quaternion.identity);
        rb.useGravity = true;
    }
}