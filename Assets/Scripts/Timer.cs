using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    public bool levelWin = false;
    private List<float> maxTimePerLevel;
    private Coroutine countdownCoroutine;
    private float maxTimeThisLevel;

    void Start()
    {
        maxTimePerLevel = new List<int> { 10f, 20.0f, 40.0f, 30.0f };
    }

    public float GetLevelTime(levelNum)
    {
        return maxTimePerLevel[levelNum-1];
    }

    public void StartTimer(levelNum)
    {
        masTimeThisLevel = maxTimePerLevel[levelNum - 1];
        countdownCoroutine = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (maxTimeThisLevel > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            maxTimeThisLevel -= 1;
            GetComponent<TextMeshProUGUI>().text = maxTime.ToString();
        }

        levelWin = true;
    }

    public void RestartTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }// Stop the ongoing countdown
        maxTimeThisLevel = 10f;
        levelWin = false;
        StartCoroutine(Countdown()); // Restart the countdown
    }
}
