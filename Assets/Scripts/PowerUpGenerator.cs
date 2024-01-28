using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject speechBubblePrefab;
    public int numberOfPowerUps = 5;
    public string[] sentences = {
        "Collect me!",
        "Power up!",
        "Great job!",
        "Boost time!",
        // Add more sentences as needed
    };

    void Start()
    {
        GeneratePowerUps();
    }

    void GeneratePowerUps()
    {
        for (int i = 0; i < numberOfPowerUps; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 20f, Random.Range(-10f, 10f));
            Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
        }
    }
}

