using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    public bool levelWin = false;
    [SerializeField]
    private float maxTime = 10f;
    private Coroutine countdownCoroutine;

    public void StartTimer()
    {
        countdownCoroutine = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (maxTime > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            maxTime -= 1;
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
        maxTime = 10f;
        levelWin = false;
        StartCoroutine(Countdown()); // Restart the countdown
    }
}
