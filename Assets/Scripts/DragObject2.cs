using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DragObject2 : MonoBehaviour
{
    private bool dragging = false;
    private float distance;
    Color originalColor;
    Color mouseDownColor;
    private Vector3 originalTransform;
    Vector3 worldPos;
    Vector3 storedWorldPos;
    Quaternion worldRotation;
    Quaternion storedWorldRotation;
    bool notFirstRespawn = false;
    Rigidbody rb;

    [SerializeField]
    protected float timeout = 0f;
    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        mouseDownColor = Color.Lerp(originalColor, Color.white, 0.3f);
        worldPos = transform.position;
        rb = GetComponent<Rigidbody>();
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
        dragging = true;

    }

    void OnMouseUp()
    {
        GetComponent<MeshRenderer>().material.color = originalColor;
        dragging = false;
        transform.parent = null;
        enabled = false;
        Vector3 lockedPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.position = lockedPosition;
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        
        Timeout();
    }
    async void Timeout()
    {
        if (timeout > 0)
        {
            await Task.Delay(Mathf.RoundToInt(1000 * timeout));
            gameObject.SetActive(false);
        }
    }

    void PassRespawnInfo()
    {
        if (notFirstRespawn)
        {
            GameObject newInstance = Instantiate(gameObject, storedWorldPos, storedWorldRotation);
            newInstance.GetComponent<DragObject2>().enabled = true;
            DragObject2 mainScript = newInstance.GetComponent<DragObject2>();
            mainScript.SetSpawnPosition(storedWorldPos, storedWorldRotation);
        }
        else
        {
            GameObject newInstance = Instantiate(gameObject, worldPos, worldRotation);
            newInstance.GetComponent<DragObject2>().enabled = true;
            DragObject2 mainScript = newInstance.GetComponent<DragObject2>();
            mainScript.SetSpawnPosition(worldPos, worldRotation);

        }
    }
    public void SetSpawnPosition(Vector3 spawnPos, Quaternion spawnRotation)
    {
        storedWorldPos = spawnPos;
        storedWorldRotation = spawnRotation;
        notFirstRespawn = true;

    }
}
