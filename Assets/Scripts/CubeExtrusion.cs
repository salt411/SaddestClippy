using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExtrusion : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Material material;

    public float extrusionAmount = 1f;

    void Start()
    {
        // Get the mesh component of the cube
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // Get the material component of the cube
        material = GetComponent<Renderer>().material;

        // Call the custom methods to extrude the front corners and change color
        ExtrudeFrontCorners();
        ChangeToRandomRetroColor();
    }

    void ExtrudeFrontCorners()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            // Check if the vertex is in the front face of the cube
            if (vertices[i].z >= 0.5f)
            {
                // Extrude the vertex along its normal (z-axis)
                vertices[i] += vertices[i].normalized * extrusionAmount;
            }
        }

        // Update the mesh with the modified vertices
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void ChangeToRandomRetroColor()
    {
        // Generate a random retro color
        Color randomColor = new Color(
            Random.Range(0.5f, 1f),
            Random.Range(0.5f, 1f),
            Random.Range(0.5f, 1f)
        );

        // Set the material color to the random color
        material.color = randomColor;
    }
}


