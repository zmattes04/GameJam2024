using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTilt : MonoBehaviour
{
    private Vector3 rotation = new Vector3(0f, 0f, 0f);
    private float rotX;
    private float rotY;
    public float verticalRotationSpeed;
    public float horizontalRotationSpeed;

    private void Update()
    {
        rotX = Input.GetAxis("Mouse Y") * verticalRotationSpeed;
        rotY = Input.GetAxis("Mouse X") * -horizontalRotationSpeed;
        rotation.x = rotX;
        rotation.y = 0;
        rotation.z = rotY;
        transform.Rotate(rotation * Time.deltaTime);
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y = 0;
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}

