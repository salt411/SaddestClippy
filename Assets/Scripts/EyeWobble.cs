using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeWobble : MonoBehaviour
{
    public Transform leftEye;
    public Transform rightEye;
    public float wobbleFrequency = 2f; // Adjust the frequency of the wobble
    public float wobbleAmplitude = 0.02f; // Adjust the amplitude of the wobble

    private Vector3 leftEyeOriginalPosition;
    private Vector3 rightEyeOriginalPosition;

    void Start()
    {
        // Save the original positions of the eyes
        leftEyeOriginalPosition = leftEye.localPosition;
        rightEyeOriginalPosition = rightEye.localPosition;
    }

    void Update()
    {
        // Generate random offsets using Perlin noise
        float time = Time.time * wobbleFrequency;
        float xOffset = Mathf.PerlinNoise(time, 0f) * wobbleAmplitude;
        float yOffset = Mathf.PerlinNoise(0f, time) * wobbleAmplitude;

        // Apply the wobble to the eye positions
        leftEye.localPosition = leftEyeOriginalPosition + new Vector3(xOffset, yOffset, 0f);
        rightEye.localPosition = rightEyeOriginalPosition + new Vector3(xOffset, yOffset, 0f);
    }
}
