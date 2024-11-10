using UnityEngine;
using Parabox.CSG;

public class GenerateHoles : MonoBehaviour
{
    public float minX, maxX, minZ, maxZ;

    public GameObject PerformSubtraction(GameObject targetObject, GameObject subtractMeshObject, Vector3 scale)
    {
        float xPos = Random.Range(minX, maxX);
        float zPos = Random.Range(minZ, maxZ);
        Vector3 newPosition = new Vector3(xPos, subtractMeshObject.transform.position.y, zPos);
        subtractMeshObject.transform.position = newPosition;
        Model result = CSG.Subtract(targetObject, subtractMeshObject);
        targetObject.GetComponent<MeshFilter>().sharedMesh = result.mesh;
        targetObject.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        targetObject.GetComponent<MeshCollider>().sharedMesh = null;
        targetObject.GetComponent<MeshCollider>().sharedMesh = targetObject.GetComponent<MeshFilter>().sharedMesh;
        targetObject.transform.localScale = scale;
        return targetObject;
    }
}
