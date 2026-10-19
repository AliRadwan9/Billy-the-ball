using UnityEngine;

public class SkyGradient : MonoBehaviour
{
    public Color topColor = Color.blue;
    public Color bottomColor = Color.cyan;

    private Mesh mesh; 

    void Start()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter != null)
        {
            mesh = filter.mesh;
            ApplyGradient();
        }
    }

    void ApplyGradient()
    {
        if (mesh == null) return;

        Color[] colors = new Color[4];
        colors[0] = bottomColor; // Bottom Left
        colors[1] = bottomColor; // Bottom Right
        colors[2] = topColor;    // Top Left
        colors[3] = topColor;    // Top Right

        mesh.colors = colors;
    }
    
    // This ensures that the gradient updates in the editor when colors are changed
    void OnValidate() 
    {
        if (mesh != null) ApplyGradient();
    }
}