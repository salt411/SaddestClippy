using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDistorter : MonoBehaviour
{
    public float waveFrequency = 2f;
    public float waveAmplitude = 0.5f;
    public float waveSpeed = 1f;
    public float middleBendFactor = 2f; // Adjust this to control the sharpness of the bend
    public int numWrinkleLayers = 3;
    public float wrinkleFrequencyMultiplier = 2f;
    public float wrinkleAmplitudeMultiplier = 0.2f;
    public float jumpForce = 5f; // Adjust this value for jump height
    public float jumpDamping = 0.9f; // Adjust this value for jump damping

    private Vector3[] originalVertices;
    private MeshFilter meshFilter;
    private bool isJumping = false;
    private float jumpStartTime;
    private Rigidbody rb;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        rb = GetComponent<Rigidbody>();

        if (meshFilter != null)
        {
            // Save the original vertices for later reference
            originalVertices = meshFilter.mesh.vertices;
        }
        else
        {
            Debug.LogError("MeshFilter not found on MagicCarpet GameObject.");
        }
    }

    void Update()
    {
        if (meshFilter != null)
        {
            // Get the mesh and its vertices
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;

            // Modify vertices using Perlin noise, wrinkles, and vertical input
            float time = Time.time * waveSpeed;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 originalVertex = originalVertices[i];

                float perlinValueX = Mathf.PerlinNoise(originalVertex.x * waveFrequency + time, 0f);
                float perlinValueY = Mathf.PerlinNoise(0f, originalVertex.z * waveFrequency + time);

                // Apply a stronger distortion in the middle section
                float middleFactor = 1f - Mathf.Abs(originalVertex.y / transform.localScale.y); // Adjust for scale
                middleFactor = Mathf.Pow(middleFactor, middleBendFactor);

                // Combine the Perlin noise with the middle factor for distortion
                float middleDistortion = perlinValueX * perlinValueY * waveAmplitude * middleFactor;

                // Apply wrinkles
                for (int layer = 0; layer < numWrinkleLayers; layer++)
                {
                    float wrinkleFrequency = waveFrequency * wrinkleFrequencyMultiplier * (layer + 1);
                    float wrinkleAmplitude = waveAmplitude * wrinkleAmplitudeMultiplier / (layer + 1);

                    middleDistortion += Mathf.PerlinNoise(originalVertex.x * wrinkleFrequency + time, originalVertex.z * wrinkleFrequency + time) * wrinkleAmplitude;
                }

                // Apply vertical input
                float verticalInput = Input.GetAxis("Vertical");
                float verticalMovement = verticalInput * Time.deltaTime;
                vertices[i].y = originalVertex.y + middleDistortion + verticalMovement;
            }

            // Apply the modified vertices to the mesh
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            // Check for jump input (Space bar)
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                Jump();
            }

            // Damping for smoother descent
            if (isJumping)
            {
                Debug.Log("issue in MeshDistorter");
                float elapsedJumpTime = Time.time - jumpStartTime;
                float dampingFactor = Mathf.Clamp01(1f - elapsedJumpTime * jumpDamping);
                rb.velocity *= dampingFactor;

                // End jump after a certain time (adjust as needed)
                if (elapsedJumpTime > 1.0f)
                {
                    isJumping = false;
                }
            }
        }
    }

    void Jump()
    {
        isJumping = true;
        jumpStartTime = Time.time;

        // Apply a vertical force to simulate a jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

