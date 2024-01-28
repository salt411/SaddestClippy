using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject lossScreen;
    public GameObject mainMenu;
    public Button restartButton;
    public Button startButton;

    public GameObject timer;
    private Timer timerScript;
    public GameObject spawnManager;
    private SpawnManager spawnManagerScript;
    public GameObject winBox;
    private ClippyWin clippyWinScript;

    public GameObject[] levelObjects;
    public GameObject[] levelUI;

    private Rigidbody playerRigidbody;
    private int currLevel;
    GameObject[] platformList;

    void Awake()
    {
        clippyWinScript = winBox.GetComponent<ClippyWin>();
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        timerScript = timer.GetComponent<Timer>();
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartLevel);

        currLevel = 1;

        lossScreen.SetActive(false);
        levelObjects[currLevel - 1].SetActive(false);
    }

    //Vector3 clippySpawnPos = new Vector3(-9f, 4f, 0f);

    void StartGame()
    {
        spawnManagerScript.SpawnClippy();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
        timerScript.StartTimer(currLevel);
        mainMenu.SetActive(false);
        levelObjects[currLevel-1].SetActive(true);
        levelUI[currLevel-1].SetActive(true);
        spawnManagerScript.SpawnObstacles();
    }

    void RestartLevel()
    {
        clippyWinScript.hasWon = false;
        lossScreen.SetActive(false);
        StartGame();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (clippyWinScript.hasWon) // if Clippy succeeds
        {
            timerScript.StopTimer();
            lossScreen.SetActive(true);
            levelObjects[currLevel - 1].SetActive(false);
            platformList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in platformList)
            {
                Destroy(platform);
            }
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            Destroy(playerObject);

        }
        if (timerScript.levelWin) //if we succeed
        {

            levelObjects[currLevel - 1].SetActive(false);
            platformList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in platformList)
            {
                Destroy(platform);
            }
            Vector3 newVelocity = playerRigidbody.velocity;
            newVelocity.x = 0;
            playerRigidbody.velocity = newVelocity;
            Debug.Log(playerRigidbody.position.y);
            if (playerRigidbody.position.y < -2.5f)
            {
                NextLevel();
            }

        }
    }
    void NextLevel()
    {
        //levelObjects[currLevel-1].SetActive(false);
        //levelObjects[currLevel].SetActive(true);
        //levelUI[currLevel].SetActive(true);
        levelUI[currLevel-1].SetActive(false);
        currLevel++;
        timerScript.levelWin = false;
        StartGame();
    }

   
}
