using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ballPrefabs;
    private GameObject ballToInstantiate;

    private float spawnLimitXLeft = -29;
    private float spawnLimitXRight = 5;
    private float spawnPosY = 30;

    //private float startDelay = 1.0f;
    //private float spawnInterval = 4.0f;
    
    private float counter = 0f;
    private float nextWaitTime = 5f;
    
    [Range(2, 5), SerializeField, Tooltip("Limites de espera de tiempo de Spawn de pelotas")]
    private float minWaitTime = 3f, maxWaitTime = 5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(ballPrefabs.Count==0) Debug.LogError("ERROR: (SpawnManagerX.cs) No hay bolas para instanciar!!!!!");
        //InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= nextWaitTime)
        {
            SpawnRandomBall();
            counter = 0;
            nextWaitTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        ballToInstantiate = ballPrefabs[Random.Range(0, ballPrefabs.Count)];
        Instantiate(ballToInstantiate, spawnPos, ballToInstantiate.transform.rotation);
    }

}
