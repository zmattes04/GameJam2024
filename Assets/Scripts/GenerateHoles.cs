using UnityEngine;
using Parabox.CSG;
using System.Collections.Generic;

public class GenerateHoles : MonoBehaviour
{
    private List<GameObject> holePositions = new List<GameObject>();
    public GameObject holePositionPrefab;

    public GameObject PerformSubtraction(GameObject targetObject, GameObject subtractMeshObject, Vector3 scale, float minX, float maxX, float minZ, float maxZ)
    {
        Vector3 newPosition;
        float xPos, zPos;
        const float minDistanceFromBall = 1.0f;

        do
        {
            xPos = Random.Range(minX, maxX);
            zPos = Random.Range(minZ, maxZ);
            newPosition = new Vector3(xPos, subtractMeshObject.transform.position.y, zPos);
        }
        while (Vector3.Distance(newPosition, Vector3.zero) < minDistanceFromBall);

        subtractMeshObject.transform.position = newPosition;
        GameObject holeObject = Instantiate(holePositionPrefab, newPosition, Quaternion.identity);
        holePositions.Add(holeObject);

        Model result = CSG.Subtract(targetObject, subtractMeshObject);
        targetObject.GetComponent<MeshFilter>().sharedMesh = result.mesh;
        targetObject.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        targetObject.GetComponent<MeshCollider>().sharedMesh = null;
        targetObject.GetComponent<MeshCollider>().sharedMesh = targetObject.GetComponent<MeshFilter>().sharedMesh;
        targetObject.transform.localScale = scale;
        return targetObject;
    }

    public List<GameObject> getHolePositions()
    {
        return holePositions;
    }
}
