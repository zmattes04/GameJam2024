using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPhaseThrough : MonoBehaviour
{
    public float yPosPhaseThrough;

    private void OnTriggerStay(Collider other)
    {
        /*if (other.CompareTag("Ball"))
        {
            Vector3 newPosition = other.transform.position;
            newPosition.y += yPosPhaseThrough;
            other.transform.position = newPosition;
            Debug.Log("ONTRIGGERSTAY");
        } else */
        if (other.CompareTag("Board"))
        {
            Vector3 newPosition = transform.position;
            newPosition.y += yPosPhaseThrough;
            transform.position = newPosition;
            Debug.Log("ONTRIGGERSTAY");
        }
    }
}
