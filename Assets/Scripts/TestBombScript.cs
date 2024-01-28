using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBombScript : MonoBehaviour
{
    public float explosionForce = 1000f;
    public float blastRadius = 5f;
    public float upwardModifier = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(gameObject); // Destroy the bomb after explosion
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius, upwardModifier);
            }
        }
    }
}