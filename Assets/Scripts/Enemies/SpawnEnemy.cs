using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;

    public float startSpawnerInterval;
    private float spawnerInterval;

    public int numberOfEnemies;
    public static int nowTheEnemies;

    private int randEnemy;
    private int randPoint;

    private void Start()
    {
        spawnerInterval = startSpawnerInterval;
    }

    private void Update()
    {
        if (spawnerInterval <= 0 && nowTheEnemies < numberOfEnemies)
        {
            randEnemy = Random.Range(0, enemies.Length);
            randPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(enemies[randEnemy], spawnPoints[randPoint].transform.position, Quaternion.identity);

            spawnerInterval = startSpawnerInterval;
            nowTheEnemies++;
        }
        else
        {
            spawnerInterval -= Time.deltaTime;
        }
    }

}
