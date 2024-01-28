using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Start is called before the first frame update
    public GameObject winBox;
    public GameObject lossScreen;
    public GameObject level;
    public GameObject obstacleInterface;
    public GameObject timer;
    public GameObject clippy;
    public GameObject mainMenu;
    public Button restartButton;
    public Button startButton;
    private ClippyWin clippyWinScript;
    private Timer timerScript;
    GameObject[] platformList;
    void Awake()
    {
        clippyWinScript = winBox.GetComponent<ClippyWin>();
        timerScript = timer.GetComponent<Timer>();
    }

    void Start()
    {
        lossScreen.SetActive(false);
        level.SetActive(false);
        obstacleInterface.SetActive(false);
        startButton.onClick.AddListener(StartLevel);
        restartButton.onClick.AddListener(RestartLevel);

    }


    Vector3 clippySpawnPos = new Vector3(-9f, 4f, 0f);

    void StartLevel()
    {
        Instantiate(clippy, clippySpawnPos, Quaternion.identity);
        timerScript.StartTimer();
        mainMenu.SetActive(false);
        obstacleInterface.SetActive(true);
        level.SetActive(true);

    }

    void RestartLevel()
    {
        Instantiate(clippy, clippySpawnPos, Quaternion.identity);
        timerScript.RestartTimer();
        clippyWinScript.hasWon = false;
        obstacleInterface.SetActive(true);
        level.SetActive(true);
        lossScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (clippyWinScript.hasWon)
        {
            lossScreen.SetActive(true);
            level.SetActive(false);
            obstacleInterface.SetActive(false);
            platformList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in platformList)
            {
                Destroy(platform);
            }


        }
        if (timerScript.levelWin)
        {
            level.SetActive(false);
            obstacleInterface.SetActive(false);
            platformList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in platformList)
            {
                Destroy(platform);
            }

        }
    }
}
