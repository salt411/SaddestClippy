using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    public GameObject keyboardKeyPrefab;
    public GameObject speechBubblePrefab;
    public int numberOfPowerUps = 5;
    public float spawnHeight = 20f;
    public float minRotationSpeed = 30f;
    public float maxRotationSpeed = 180f;
    public float minMomentum = 5f;
    public float maxMomentum = 10f;

    void Start()
    {
        GeneratePowerUps();
    }

    void GeneratePowerUps()
    {
        for (int i = 0; i < numberOfPowerUps; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), spawnHeight, Random.Range(-10f, 10f));
            GameObject powerUp = Instantiate(keyboardKeyPrefab, randomPosition, Quaternion.identity);

            // Apply random rotation to the power-up
            powerUp.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Apply random rotation speed
            float rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            powerUp.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, rotationSpeed, 0f);

            // Apply random momentum
            float momentum = Random.Range(minMomentum, maxMomentum);
            powerUp.GetComponent<Rigidbody>().velocity = Vector3.down * momentum;
        }
    }
}


