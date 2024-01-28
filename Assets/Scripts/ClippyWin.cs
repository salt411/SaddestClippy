using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippyWin : MonoBehaviour
{
    public bool hasWon = false;
    private void OnTriggerEnter(Collider other)
    {
        var clippy = other.GetComponent<Clippy>();
        if (clippy != null)
        {
            Debug.Log("Lose!");
            hasWon = true;
        }
    }
}