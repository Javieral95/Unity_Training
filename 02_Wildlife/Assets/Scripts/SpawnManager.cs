using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemies;

    private float xRange = 14.5f;
    private float xPosition = 0f;
    private float yPosition;
    private float zPosition;
    
    private GameObject enemyToInstanciate;
    [SerializeField]
    private float startDelay = 2f; //2 segundos hasta que comienza el juego
    [SerializeField, Range(0.5f, 3f)]
    private float spawnInterval = 1.5f; //Intervalo de tiempo en el que se van generando enemigos

    private void Start()
    {
        yPosition = this.transform.position.y;
        zPosition = this.transform.position.z;
        
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval); //Metodo a invocar, tiempo a esperar antes de la primera llamada, ratio repeticion
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnRandomAnimal()
    {
        xPosition = Random.Range(-xRange, xRange);
        enemyToInstanciate = enemies[Random.Range(0, enemies.Count - 1)];
        Instantiate(enemyToInstanciate, new Vector3(xPosition, this.transform.position.y, zPosition),
            enemyToInstanciate.transform.rotation);
    }
}