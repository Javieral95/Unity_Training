using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float spawnRange = 9;
    private int numberOfEnemyInWave = 1;

    public GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            numberOfEnemyInWave++;
            WaitForNextWave();
            SpawnEnemyWave(numberOfEnemyInWave);
            Instantiate(powerUpPrefab, GenerateRandomSpawnPos(), powerUpPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Genera una oleada de enemigos en pantalla.
    /// </summary>
    /// <param name="enemyCount">Numero de enemigos a spawnear</param>
    private void SpawnEnemyWave(int enemyCount)
    {
        Vector3 randomPos = Vector3.zero;
        for (int i = 0; i < enemyCount; i++)
        {
            randomPos = GenerateRandomSpawnPos();
            Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Genera una posicion aleatoria con Y=0 y X y Z en un rango [-spawnRange, spawnRange]
    /// </summary>
    /// <returns></returns>
    private Vector3 GenerateRandomSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(2);
    }
}