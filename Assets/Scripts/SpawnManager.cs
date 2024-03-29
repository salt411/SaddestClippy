using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SpawnManager : MonoBehaviour
{
    public GameObject block;
    public GameObject pinwheel;
    public GameObject bomb;
    public GameObject clippy;
    public GameObject shortyclip;
    public GameObject spBub;
    public LevelSpawnInfo[] levelSpawnInfos;

    private Vector3 bposOffset = new Vector3(1, 2, -2);

    [System.Serializable]
    public class LevelSpawnInfo
    {
        public Vector3 spawnPosition;
        public Vector3 spawnScale;
        public GameObject clippyType;
    }

    public void SpawnClippy()
    {
        Instantiate(clippy);
    }

    public void SpawnShortyClip()
    {
        Instantiate(shortyclip);
    }

    public void SpawnSpecialClippy(int level)
    {
        if (level < 1 || level > levelSpawnInfos.Length)
        {
            Debug.LogError("Invalid level number");
            return;
        }

        LevelSpawnInfo spawnInfo = levelSpawnInfos[level - 1];
        GameObject specialClippy = Instantiate(spawnInfo.clippyType, spawnInfo.spawnPosition, Quaternion.identity);
        specialClippy.transform.localScale = spawnInfo.spawnScale;
    }

    public void SpawnObstacles()
    {
        Instantiate(block);
        Instantiate(pinwheel);
        Instantiate(bomb);
    }

    public void SpawnBlock()
    {
        Instantiate(block);
    }
    public void SpawnPinwheel()
    {
        Instantiate(pinwheel);
    }
    public void SpawnBomb()
    {
        Instantiate(bomb);
    }

    public void SpawnSpeechBubble(Vector3 bpos)
    {
        Vector3 spawnLocation = bpos + bposOffset;   
        // Instantiate spBub at a free location
        Instantiate(spBub, spawnLocation, spBub.transform.rotation);      
    }


    // Update is called once per frame

}
