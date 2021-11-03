using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    //private float spawnDelay = 2;
    //private float spawnInterval = 1.5f;

    private PlayerControllerX playerControllerScript;
    private RepeatBackgroundX repeatBackgroundScript; //For y Limits

    private Vector3 spawnPos;

    private float count = 0f;
    private float timeToSpawn = 2f;
    
    private float minSpawnInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.position;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        repeatBackgroundScript = GameObject.FindWithTag("Background").GetComponent<RepeatBackgroundX>();
        
        //InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
    }

    private void Update()
    {
        count += Time.deltaTime;

        //More time playing = More difficult game = Less interval spawn time (min 0.5 seconds)
        if (count >= timeToSpawn)
        {
            timeToSpawn = Math.Max(minSpawnInterval, timeToSpawn - (Time.deltaTime*10));
            count = 0;
            SpawnObjects();
        }
    }

    // Spawn obstacles
    void SpawnObjects()
    {
        // If game is still active, spawn new object
        if (!playerControllerScript.GameOver)
        {
            // Set random spawn location and random object index
            // The objects will spawn between the spawnmanager position and the background's height.
            Vector3 spawnLocation = new Vector3(spawnPos.x,
                Random.Range(spawnPos.y, repeatBackgroundScript.BackgroundHeight), spawnPos.z);
            int index = Random.Range(0, objectPrefabs.Length);

            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}