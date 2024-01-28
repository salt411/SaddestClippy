using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool dragging = false;
    private float distance;
    Color originalColor;
    Color mouseDownColor;
    private Vector3 originalTransform;
    Vector3 worldPos;
    Vector3 storedWorldPos;
    bool notFirstRespawn = false;

    [SerializeField]
    protected float timeout = 0f;
    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        mouseDownColor = Color.Lerp(originalColor, Color.white, 0.3f);

    }
    IEnumerator Start()
    {
        SlotDebugger parentScript = GetComponentInParent<SlotDebugger>();
        yield return new WaitForEndOfFrame();  // Wait for the parent's coroutine to complete
        worldPos = parentScript.GetPosition();

    }
    public void SetSpawnPosition(Vector3 spawnPos)
    {
        storedWorldPos = spawnPos;
        notFirstRespawn = true;

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
        if (notFirstRespawn)
        {
            GameObject newInstance = Instantiate(gameObject, storedWorldPos, Quaternion.identity);
            newInstance.GetComponent<DragObject>().enabled = true;
            DragObject mainScript = newInstance.GetComponent<DragObject>();
            mainScript.SetSpawnPosition(storedWorldPos);
        }
        else
        {
            GameObject newInstance = Instantiate(gameObject, worldPos, Quaternion.identity);
            newInstance.GetComponent<DragObject>().enabled = true;
            DragObject mainScript = newInstance.GetComponent<DragObject>();
            mainScript.SetSpawnPosition(worldPos);

        }
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


    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, rayPoint.y, transform.position.z);
        }
    }
}
