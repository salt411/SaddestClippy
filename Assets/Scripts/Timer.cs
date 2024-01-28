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
    public GameObject gameManager;
    private GameManager gameManagerScript;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent < GameManager > ();
        maxTimePerLevel = new List<float> { 3f, 20.0f, 40.0f, 30.0f };
    }

    public float GetLevelTime(int levelNum)
    {
        return maxTimePerLevel[levelNum-1];
    }

    public void StartTimer(int levelNum)
    {
        maxTimeThisLevel = maxTimePerLevel[levelNum - 1];
        countdownCoroutine = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (maxTimeThisLevel > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            maxTimeThisLevel -= 1;
            GetComponent<TextMeshProUGUI>().text = maxTimeThisLevel.ToString();
        }

        levelWin = true;
        
    }

    public void StopTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }// Stop the ongoing countdown
        levelWin = false;
    }

    //public void RestartTimer(int levelNum)
    //{
    //    maxTimeThisLevel = maxTimePerLevel[levelNum - 1];
    //    StartCoroutine(Countdown()); // Restart the countdown
    //}

}
