using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] float startSpawnTime = 1f, spawnTime = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), startSpawnTime, spawnTime);
    }

    void SpawnEnemy()
    {
        int randSpawn = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randSpawn], transform.position, Quaternion.identity);
    }
}
