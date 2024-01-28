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
    public GameObject timer;
    public GameObject mainMenu;
    public GameObject spawnManager;
    private SpawnManager spawnManagerScript;
    public Button restartButton;
    public Button startButton;
    private ClippyWin clippyWinScript;
    private Timer timerScript;
    private int pastLevel;
    private int levelCount;
    private int currLevel;
    GameObject[] platformList;
    void Awake()
    {
        clippyWinScript = winBox.GetComponent<ClippyWin>();
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        timerScript = timer.GetComponent<Timer>();
    }

    void Start()
    {
        levelCount = 1;
        lossScreen.SetActive(false);
        level.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartLevel);

    }


    Vector3 clippySpawnPos = new Vector3(-9f, 4f, 0f);

    void StartGame()
    {
        spawnManagerScript.SpawnClippy();
        timerScript.StartTimer();
        mainMenu.SetActive(false);
        level.SetActive(true);
        spawnManagerScript.SpawnObstacles();


    }

    void RestartLevel()
    {
        spawnManagerScript.SpawnClippy();
        timerScript.RestartTimer();
        clippyWinScript.hasWon = false;
        level.SetActive(true);
        lossScreen.SetActive(false);
        spawnManagerScript.SpawnObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        if (clippyWinScript.hasWon)
        {
            lossScreen.SetActive(true);
            level.SetActive(false);
            platformList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in platformList)
            {
                Destroy(platform);
            }


        }
        if (timerScript.levelWin)
        {
            level.SetActive(false);
            platformList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in platformList)
            {
                Destroy(platform);
            }

        }
    }
    void NextLevel()
    {
        pastLevel = currLevel;
        currLevel++;
        GameObject.Find("Level " + pastLevel).SetActive(false);
        GameObject.Find("Level " + currLevel).SetActive(true);
        spawnManagerScript.SpawnClippy();
    }
}
