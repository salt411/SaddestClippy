using System.Collections;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    public GameObject keyboardKeyPrefab;
    public int maxPowerUpsPerSpawn = 3;
    public float spawnIntervalMin = 5f;
    public float spawnIntervalMax = 10f;
    public float minRotationSpeed = 30f;
    public float maxRotationSpeed = 180f;
    public float minMomentum = 5f;
    public float maxMomentum = 10f;

    void Start()
    {
        // Start the coroutine for spawning power-ups
        StartCoroutine(SpawnPowerUpsCoroutine());
    }

    IEnumerator SpawnPowerUpsCoroutine()
    {
        while (true)
        {
            // Wait for a random interval before spawning
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            // Spawn a random number of keyboard keys
            int numPowerUpsToSpawn = Random.Range(1, maxPowerUpsPerSpawn + 1);
            for (int i = 0; i < numPowerUpsToSpawn; i++)
            {
                SpawnKeyboardKey();
            }
        }
    }

    void SpawnKeyboardKey()
    {
        // Spawn a keyboard key at a random position
        Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 15f, -1f);
        GameObject keyboardKey = Instantiate(keyboardKeyPrefab, randomPosition, Quaternion.identity);

        // Apply random rotation speed and momentum for the keyboard key
        float rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        keyboardKey.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, rotationSpeed, 0f);

        float momentum = Random.Range(minMomentum, maxMomentum);
        keyboardKey.GetComponent<Rigidbody>().velocity = Vector3.down * momentum;
    }
}



