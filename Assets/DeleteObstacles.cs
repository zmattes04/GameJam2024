using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObstacles : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
