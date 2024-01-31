using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBurst : MonoBehaviour
{
    public float expansionSpeed = 0.1f;
    public Vector3 maxScale = new Vector3(3f, 3f, 3f);
    public float movementSpeed = 5.0f;
    private Vector3 randomDirection;
    public float lifetime = 5f;

    private void Start()
    {
        ChangeDirection();
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        if (transform.localScale.x < maxScale.x && transform.localScale.y < maxScale.y && transform.localScale.z < maxScale.z)
        {
            transform.localScale += new Vector3(1, 1, 1) * expansionSpeed * Time.deltaTime;
        }
        if (transform.localScale.x < maxScale.x) // Only expand to maxScale
        {
            transform.localScale += new Vector3(1, 1, 1) * expansionSpeed * Time.deltaTime;
        }

        transform.Translate(randomDirection * movementSpeed * Time.deltaTime, Space.World);

        if (Random.Range(0, 100) < 2) // Change direction randomly
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }
}
