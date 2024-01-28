using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private SpeechBubbleGenerator speechGen;

    private void Awake()
    {
        speechGen = GameObject.Find("PowerUpGenerator").GetComponent<SpeechBubbleGenerator>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            speechGen.GenerateSpeechBubble(other.transform.position);
        }
    }
}
