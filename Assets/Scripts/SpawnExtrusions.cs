using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExtrusions : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    private int numObjects;
    public float yMin;
    public float yMax;
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
    public Transform spawnPosition;

    private float xPos;
    private float yPos;
    private float zPos;
    public int needsXRotationIndex;

    void Start()
    {
        numObjects = 1 + (PlayerPrefs.GetInt("Difficulty", 1) / 2);
        for (int i = 0; i < numObjects; i++)
        {
            int index = Random.Range(0, objectsToSpawn.Count);
            SpawnObject(index);
        }
    }

    private void SpawnObject(int index)
    {
        xPos = Random.Range(xMin, xMax);
        yPos = objectsToSpawn[index].GetComponent<ExtrusionYPos>().yPos;
        zPos = Random.Range(zMin, zMax);
        spawnPosition.position = new Vector3(xPos, yPos, zPos);
        GameObject spawnedObject = Instantiate(objectsToSpawn[index], spawnPosition.position, Quaternion.identity);
        if (index > needsXRotationIndex)
        {
            spawnedObject.transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
        }
        spawnedObject.transform.SetParent(this.transform);
    }
}

