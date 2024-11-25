using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ON TRIGGER ENTER");
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
