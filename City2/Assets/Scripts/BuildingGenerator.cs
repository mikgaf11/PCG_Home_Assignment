using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public int width = 5; // Width of the building in blocks
    public int depth = 5; // Depth of the building in blocks
    public int minHeight = 3; // Minimum height of the building in blocks
    public int maxHeight = 5; // Maximum height of the building in blocks

    private Color brown = new Color(0.65f, 0.16f, 0.16f); // Define a custom brown color

    public void GenerateBuildingAtPosition(Vector3 position)
    {
        // Determine the height of the building
        int height = Random.Range(minHeight, maxHeight + 1);

        // Generate the base structure of the building
        GenerateBaseStructure(position, height);

        // Generate the roof
        GenerateRoof(position, height);
    }

    void GenerateBaseStructure(Vector3 position, int height)
    {
        // Loop through each position in the width and depth
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                // Generate the walls
                for (int y = 0; y < height; y++)
                {
                    // Only create outer walls
                    if (x == 0 || x == width - 1 || z == 0 || z == depth - 1)
                    {
                        Vector3 blockPosition = position + new Vector3(x, y, z);
                        CreateCube(blockPosition);
                    }
                }

                // Generate columns at the corners
                if ((x == 0 || x == width - 1) && (z == 0 || z == depth - 1))
                {
                    for (int y = 0; y < height; y++)
                    {
                        Vector3 blockPosition = position + new Vector3(x, y, z);
                        CreateCylinder(blockPosition);
                    }
                }

                // Generate windows
                if (x != 0 && x != width - 1 && z != 0 && z != depth - 1 && Random.value > 0.5f)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        Vector3 blockPosition = position + new Vector3(x, y, z);
                        CreateCube(blockPosition, Color.blue); // Use blue color for windows
                    }
                }
            }
        }

        // Generate a door
        CreateCube(position + new Vector3(width / 2, 0, 0), brown); // Door at the center front
        CreateCube(position + new Vector3(width / 2, 1, 0), brown); // Door at the center front
    }

    void GenerateRoof(Vector3 position, int height)
    {
        // Create a pyramid-shaped roof
        for (int y = 0; y < height / 2; y++)
        {
            for (int x = -y; x < width + y; x++)
            {
                for (int z = -y; z < depth + y; z++)
                {
                    Vector3 blockPosition = position + new Vector3(x, height + y, z);
                    CreatePyramid(blockPosition);
                }
            }
        }
    }

    void CreateCube(Vector3 position, Color color = default(Color))
    {
        // Create a cube at the specified position with an optional color
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        if (color != default(Color))
        {
            cube.GetComponent<Renderer>().material.color = color;
        }
        cube.transform.parent = this.transform;
    }

    void CreateCylinder(Vector3 position)
    {
        // Create a cylinder at the specified position
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = position;
        cylinder.transform.parent = this.transform;
    }

    void CreatePyramid(Vector3 position)
    {
        // Create a pyramid-like structure at the specified position using a scaled cube
        GameObject pyramid = GameObject.CreatePrimitive(PrimitiveType.Cube); // Temporary using cube
        pyramid.transform.position = position;
        pyramid.transform.localScale = new Vector3(1, 0.5f, 1);
        pyramid.transform.parent = this.transform;
    }
}
