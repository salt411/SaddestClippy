using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DragBomb : MonoBehaviour
{
    // Start is called before the first frame update
    private bool dragging = false;
    private float distance;
    Color originalColor;
    Color mouseDownColor;
    private Vector3 originalTransform;
    private SpawnManager spawnManagerScript;
    Vector3 worldPos;
    Vector3 storedWorldPos;
    Quaternion worldRotation;
    Quaternion storedWorldRotation;
    //bool notFirstRespawn = false;
    bool isArmed = false;
    Rigidbody rb;
    Collider bombCollider;

    public GameObject explosionPrefab;
    public float explosionForce = 1000f;
    public float blastRadius = 5f;
    public float upwardModifier = 2f;


    [SerializeField]
    protected float timeout = 0f;
    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        mouseDownColor = Color.Lerp(originalColor, Color.white, -0.8f);
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        worldPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        bombCollider = GetComponent<Collider>();
        worldRotation = transform.rotation;
    }
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, rayPoint.y, transform.position.z);
        }
    }


    void OnMouseDown()
    {
        GetComponent<MeshRenderer>().material.color = mouseDownColor;
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        bombCollider.enabled = false;
        dragging = true;

    }

    void OnMouseUp()
    {
        isArmed = true;
        bombCollider.enabled = true;
        GetComponent<MeshRenderer>().material.color = originalColor;
        dragging = false;
        transform.parent = null;
        enabled = false;
        Vector3 lockedPosition = new Vector3(transform.position.x, transform.position.y, -0.5f);
        transform.position = lockedPosition;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;
        spawnManagerScript.SpawnBomb();
        //PassRespawnInfo();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isArmed)
        {
            Explode();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("check");
            Destroy(gameObject);
        }
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (var coll in colliders)
        {
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius, upwardModifier);
            }
        }
    }

    //void PassRespawnInfo()
    //{
    //    if (notFirstRespawn)
    //    {
    //        GameObject newInstance = Instantiate(gameObject, storedWorldPos, storedWorldRotation);
    //        newInstance.GetComponent<DragBomb>().enabled = true;
    //        DragBomb mainScript = newInstance.GetComponent<DragBomb>();
    //        mainScript.SetSpawnPosition(storedWorldPos, storedWorldRotation);
    //    }
    //    else
    //    {
    //        GameObject newInstance = Instantiate(gameObject, worldPos, worldRotation);
    //        newInstance.GetComponent<DragBomb>().enabled = true;
    //        DragBomb mainScript = newInstance.GetComponent<DragBomb>();
    //        mainScript.SetSpawnPosition(worldPos, worldRotation);

    //    }
    //}
    //public void SetSpawnPosition(Vector3 spawnPos, Quaternion spawnRotation)
    //{
    //    storedWorldPos = spawnPos;
    //    storedWorldRotation = spawnRotation;
    //    notFirstRespawn = true;

    //}
}