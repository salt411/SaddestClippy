using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private SpawnManager spawnMan;

    private void Awake()
    {
        spawnMan = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            spawnMan.SpawnSpeechBubble(other.transform.position);
        }
    }
}
