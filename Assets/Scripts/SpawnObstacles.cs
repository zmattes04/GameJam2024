using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public List<GameObject> objectsList;
    public float spawnInterval, spawnIntervalDecrement, spawnIntervalMin;
    public int currentMaxSpawnPerInterval;
    public int maxMaxSpawnPerInterval;
    public int minSpawnPerInterval;
    public float yMin, yMax, xMin, xMax, zMin, zMax;
    private Transform spawnPosition;

    private float xPos, yPos, zPos;
    private float timer = 0f;
    private int objectsIndex;

    public SoundEffectPlayer soundEffectPlayer;
    public SoundEffectType soundEffectType;


    void Start()
    {
        spawnPosition = this.transform;
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
            if (spawnInterval > spawnIntervalMin)
            {
                spawnInterval -= spawnIntervalDecrement;
            }
            if (currentMaxSpawnPerInterval <= maxMaxSpawnPerInterval)
            {
                currentMaxSpawnPerInterval++;
            }
        }
    }

    private void SpawnObject()
    {
        int numberToSpawn = Random.Range(minSpawnPerInterval, currentMaxSpawnPerInterval);

        for (int i = 1; i <= numberToSpawn; i++)
        {
            objectsIndex = Random.Range(0, objectsList.Count);
            xPos = Random.Range(xMin, xMax);
            yPos = Random.Range(yMin, yMax);
            zPos = Random.Range(zMin, zMax);
            spawnPosition.position = new Vector3(xPos, yPos, zPos);
            Instantiate(objectsList[objectsIndex], spawnPosition.position, Quaternion.identity);
        }
        soundEffectPlayer.PlaySoundEffect(soundEffectType);
    }
}

