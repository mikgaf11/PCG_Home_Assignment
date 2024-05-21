using UnityEngine;

public class TownManager : MonoBehaviour
{
    public GameObject roadStraightPrefab;
    public GameObject roadIntersectionPrefab;
    public GameObject[] vegetationPrefabs;
    public GameObject[] propPrefabs;

    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 20f; // Larger cell size for more space
    public float buildingSpacing = 5f;
    public float buildingDensity = 0.1f; // Lower density for fewer buildings
    public float vegetationDensity = 0.1f; // Density for vegetation

    private BuildingGenerator buildingGenerator;

    void Start()
    {
        // Find the BuildingGenerator GameObject and get the DetailedBuildingGenerator script
        buildingGenerator = FindObjectOfType<BuildingGenerator>();
        if (buildingGenerator == null)
        {
            Debug.LogError("DetailedBuildingGenerator script not found in the scene.");
            return;
        }

        GenerateTown();
    }

    void GenerateTown()
    {
        GenerateCentralBuilding();
        GenerateRoads();
        GenerateBuildingsAndVegetation();
    }

    void GenerateCentralBuilding()
    {
        Vector3 centralPosition = new Vector3(gridWidth * cellSize / 2, 0, gridHeight * cellSize / 2);
        buildingGenerator.GenerateBuildingAtPosition(centralPosition);
        GenerateSurroundingRoads(centralPosition);
    }

    void GenerateRoads()
    {
        for (int x = 0; x <= gridWidth; x++)
        {
            for (int z = 0; z <= gridHeight; z++)
            {
                Vector3 position = new Vector3(x * cellSize, 0, z * cellSize);

                if (x < gridWidth && z < gridHeight)
                {
                    Instantiate(roadIntersectionPrefab, position, Quaternion.identity);
                }

                if (x < gridWidth)
                {
                    Vector3 roadPos = new Vector3(x * cellSize + cellSize / 2, 0, z * cellSize);
                    Instantiate(roadStraightPrefab, roadPos, Quaternion.Euler(0, 90, 0));
                }

                if (z < gridHeight)
                {
                    Vector3 roadPos = new Vector3(x * cellSize, 0, z * cellSize + cellSize / 2);
                    Instantiate(roadStraightPrefab, roadPos, Quaternion.identity);
                }
            }
        }
    }

    void GenerateSurroundingRoads(Vector3 centralPosition)
    {
        float halfCellSize = cellSize / 2;

        // Generate roads around the central building
        Instantiate(roadIntersectionPrefab, centralPosition + new Vector3(halfCellSize, 0, halfCellSize), Quaternion.identity);
        Instantiate(roadIntersectionPrefab, centralPosition + new Vector3(halfCellSize, 0, -halfCellSize), Quaternion.identity);
        Instantiate(roadIntersectionPrefab, centralPosition + new Vector3(-halfCellSize, 0, halfCellSize), Quaternion.identity);
        Instantiate(roadIntersectionPrefab, centralPosition + new Vector3(-halfCellSize, 0, -halfCellSize), Quaternion.identity);

        Instantiate(roadStraightPrefab, centralPosition + new Vector3(halfCellSize, 0, 0), Quaternion.Euler(0, 90, 0));
        Instantiate(roadStraightPrefab, centralPosition + new Vector3(-halfCellSize, 0, 0), Quaternion.Euler(0, 90, 0));
        Instantiate(roadStraightPrefab, centralPosition + new Vector3(0, 0, halfCellSize), Quaternion.identity);
        Instantiate(roadStraightPrefab, centralPosition + new Vector3(0, 0, -halfCellSize), Quaternion.identity);
    }

    void GenerateBuildingsAndVegetation()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 cellCenter = new Vector3(x * cellSize + cellSize / 2, 0, z * cellSize + cellSize / 2);

                if (Random.value < buildingDensity)
                {
                    Vector3 position = GetRandomPositionInCell(x, z);
                    buildingGenerator.GenerateBuildingAtPosition(position);
                }

                if (Random.value < vegetationDensity)
                {
                    Vector3 position = GetRandomPositionInCell(x, z);
                    GenerateVegetation(position);
                }

                GenerateProps(cellCenter);
            }
        }
    }

    Vector3 GetRandomPositionInCell(int x, int z)
    {
        float randomX = Random.Range(x * cellSize + buildingSpacing, (x + 1) * cellSize - buildingSpacing);
        float randomZ = Random.Range(z * cellSize + buildingSpacing, (z + 1) * cellSize - buildingSpacing);
        return new Vector3(randomX, 0, randomZ);
    }

    void GenerateVegetation(Vector3 position)
    {
        GameObject vegetation = Instantiate(vegetationPrefabs[Random.Range(0, vegetationPrefabs.Length)], position, Quaternion.identity);
        vegetation.transform.parent = this.transform;
    }

    void GenerateProps(Vector3 position)
    {
        if (Random.value > 0.5f)
        {
            GameObject prop = Instantiate(propPrefabs[Random.Range(0, propPrefabs.Length)], position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
            prop.transform.parent = this.transform;
        }
    }
}
