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
    public GameObject[] levelObjects;
    public GameObject[] levelUI;
    private GameObject currLevelUI;
    private GameObject currLevelObjects;
    public GameObject timer;
    public GameObject mainMenu;
    public GameObject spawnManager;
    //private GameObject playerObject;
    private Rigidbody playerRigidbody;
    private SpawnManager spawnManagerScript;
    public Button restartButton;
    public Button startButton;
    private ClippyWin clippyWinScript;
    private Timer timerScript;
    private int pastLevel;
    private int currLevel;
    private bool tooBad = false;
    GameObject[] platformList;

    void Awake()
    {
        clippyWinScript = winBox.GetComponent<ClippyWin>();
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        timerScript = timer.GetComponent<Timer>();
        currLevel = 1;
    }

    void Start()
    {
        
        lossScreen.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartLevel);
        currLevelObjects = levelObjects[currLevel-1];
        levelObjects[currLevel-1].SetActive(false);
    }


    Vector3 clippySpawnPos = new Vector3(-9f, 4f, 0f);

    void StartGame()
    {
        spawnManagerScript.SpawnClippy();
        timerScript.StartTimer(currLevel);
        mainMenu.SetActive(false);
        levelObjects[currLevel-1].SetActive(true);
        levelUI[currLevel-1].SetActive(true);
        spawnManagerScript.SpawnObstacles();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = playerObject.GetComponent<Rigidbody>();


    }

    void RestartLevel()
    {
        spawnManagerScript.SpawnClippy();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
        clippyWinScript.hasWon = false;
        currLevelObjects.SetActive(true);
        levelUI[currLevel-1].SetActive(true);
        lossScreen.SetActive(false);
        spawnManagerScript.SpawnObstacles();
        tooBad = false;
        timerScript.StartTimer(currLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (clippyWinScript.hasWon) // if Clippy succeeds
        {
            timerScript.StopTimer();
            tooBad = true;
            lossScreen.SetActive(true);
            currLevelObjects.SetActive(false);
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            Destroy(playerObject);
            platformList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in platformList)
            {
                Destroy(platform);
            }


        }
        if (timerScript.levelWin && !tooBad) //if Clippy fails
        {

            //levelObjects[currLevel - 1].SetActive(false);
            currLevelObjects.SetActive(false);
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
            //level.SetActive(false);
            //platformList = GameObject.FindGameObjectsWithTag("Platform");
            //foreach (GameObject platform in platformList)
            //{
            //    Destroy(platform);
            //}

        }
    }
    void NextLevel()
    {
        levelObjects[currLevel-1].SetActive(false);
        levelObjects[currLevel].SetActive(true);
        levelUI[currLevel-1].SetActive(false);
        levelUI[currLevel].SetActive(true);
        currLevel++;
        timerScript.levelWin = false;
        RestartLevel();
    }
}
