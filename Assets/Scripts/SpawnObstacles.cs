using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public List<GameObject> objectsList;
    public float spawnInterval, spawnIntervalDecrement, spawnIntervalMin;
    public int currentMaxSpawnPerInterval;
    public int maximumMaxSpawnPerInterval;
    public int minSpawnPerInterval;
    public float yMin, yMax, xMin, xMax, zMin, zMax;
    private Transform spawnPosition;

    private float xPos, yPos, zPos;
    private float timer = 0f;
    private int objectsIndex;

    public SoundEffectPlayer soundEffectPlayer;
    public SoundEffectType soundEffectType;
    public int initialIndex;
    private int objectIndex;
    public int timesBeforeObjectIndexIncrements;
    private int timesCounter;

    public CameraShake cameraShake;
    public LightingControl lightingControl;

    void Start()
    {
        spawnPosition = this.transform;
        timer = 0f;
        objectIndex = initialIndex;
        timesCounter = 0;
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
            if (currentMaxSpawnPerInterval <= maximumMaxSpawnPerInterval)
            {
                currentMaxSpawnPerInterval++;
            }
            timesCounter++;
            if (timesCounter >= timesBeforeObjectIndexIncrements)
            {
                if (objectIndex < objectsList.Count)
                {
                    objectIndex++;
                }
                timesCounter = 0;
            }
        }
    }

    private void SpawnObject()
    {
        int numberToSpawn = Random.Range(minSpawnPerInterval, currentMaxSpawnPerInterval);
        cameraShake.Shake(0.2f + MapIntToFloat(numberToSpawn), MapIntToFloat(numberToSpawn));
        lightingControl.FlashLights();

        for (int i = 1; i <= numberToSpawn; i++)
        {
            objectsIndex = Random.Range(0, objectIndex);
            xPos = Random.Range(xMin, xMax);
            yPos = Random.Range(yMin, yMax);
            zPos = Random.Range(zMin, zMax);
            spawnPosition.position = new Vector3(xPos, yPos, zPos);
            Instantiate(objectsList[objectsIndex], spawnPosition.position, Quaternion.identity);
        }
        soundEffectPlayer.PlaySoundEffect(soundEffectType);
    }

    float MapIntToFloat(int x)
    {
        return 0.1f + ((x - 1) / 13f) * (0.9f - 0.1f);
    }
}

