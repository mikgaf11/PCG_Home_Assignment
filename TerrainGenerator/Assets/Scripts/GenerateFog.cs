using UnityEngine;

public class GenerateFog : MonoBehaviour
{
    public Material fogMaterial;
    public Color fogColor = Color.gray;
    public float fogDensity = 0.01f;
    public float fogStartDistance = 0.0f;
    public float fogEndDistance = 100.0f;

    void Start()
    {
        UpdateFog();
    }

    void Update()
    {
        // Example: Modify fog parameters dynamically
        float time = Time.time;
        fogDensity = Mathf.Lerp(0.01f, 0.05f, Mathf.PingPong(time, 1.0f));
        fogColor = Color.Lerp(Color.gray, Color.blue, Mathf.PingPong(time, 1.0f));

        // Update fog settings
        UpdateFog();
    }

    void UpdateFog()
    {
        // Set fog properties in the material
        fogMaterial.SetColor("_FogColor", fogColor);
        fogMaterial.SetFloat("_FogDensity", fogDensity);
        fogMaterial.SetFloat("_FogStartDistance", fogStartDistance);
        fogMaterial.SetFloat("_FogEndDistance", fogEndDistance);
    }
}
