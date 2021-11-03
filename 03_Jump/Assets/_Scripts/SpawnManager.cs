using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs;

    private Vector3 spawnPos;

    private GameObject obstacleToInstantiate;
    
    private PlayerController _playerController;

    [SerializeField, Range(0,5)]
    private float startDelay = 2f;

    [SerializeField, Range(1,5)]
    private float repeatRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.position; //(30,0,0)
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void SpawnObstacle()
    {
        if (!_playerController.GameOver)
        {
            obstacleToInstantiate = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            Instantiate(obstacleToInstantiate, spawnPos, obstacleToInstantiate.transform.rotation);
        }
    }
}
