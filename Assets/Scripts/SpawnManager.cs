using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject block;
    public GameObject pinwheel;
    public GameObject bomb;

    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
