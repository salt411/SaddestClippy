using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubbleScript : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void SetSentence(string sentence)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = sentence;
        }
    }
}

