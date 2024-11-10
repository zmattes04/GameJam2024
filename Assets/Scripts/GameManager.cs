using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject board;
    public GameObject hole;
    public Vector3 boardScale;
    public int holeCount;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        for (int i = 0; i < holeCount; i++)
        {
            board.GetComponent<GenerateHoles>().PerformSubtraction(board, hole, boardScale);
        }
    }

    void Update()
    {

    }
}

