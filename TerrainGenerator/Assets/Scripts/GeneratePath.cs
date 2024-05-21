using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public Terrain terrain;
    public int numPoints = 10;
    public float pathWidth = 2f;
    public float heightOffset = 1f;

    void Start()
    {
        GeneratePath();
    }

    void GeneratePath()
    {
        Vector3[] points = new Vector3[numPoints];

        // Start and end points
        points[0] = GetRandomPointOnTerrain();
        points[numPoints - 1] = GetRandomPointOnTerrain();

        // Generate intermediate points
        for (int i = 1; i < numPoints - 1; i++)
        {
            Vector3 direction = Random.insideUnitSphere * pathWidth;
            Vector3 prevDirection = points[i - 1] - points[i];
            points[i] = points[i - 1] + Vector3.ClampMagnitude(direction, pathWidth);
        }

        // Adjust points to terrain height
        for (int i = 0; i < numPoints; i++)
        {
            points[i].y = GetHeightOnTerrain(points[i]) + heightOffset;
        }

        // Create GameObjects for path
        for (int i = 0; i < numPoints; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = points[i];
        }
    }

    Vector3 GetRandomPointOnTerrain()
    {
        Vector3 terrainSize = terrain.terrainData.size;
        return new Vector3(Random.Range(0f, terrainSize.x), 0f, Random.Range(0f, terrainSize.z));
    }

    float GetHeightOnTerrain(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(position.x, 1000, position.z), Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            return hit.point.y;
        }
        return 0f;
    }
}
