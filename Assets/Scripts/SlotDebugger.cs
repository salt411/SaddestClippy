using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 worldCenter;

    void Start()
    {
        StartCoroutine(GetPositionNextFrame());
    }

    IEnumerator GetPositionNextFrame()
    {
        // Wait until the end of the frame after all UI layout updates are done
        yield return new WaitForEndOfFrame();

        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);
        this.worldCenter = (worldCorners[0] + worldCorners[2]) / 2;
        Debug.Log("World center: " + worldCenter);
    }
    public Vector3 GetPosition()
    {
        return worldCenter;
    }

}
