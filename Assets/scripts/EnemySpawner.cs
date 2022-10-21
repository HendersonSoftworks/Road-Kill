using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private GameObject[] spawners;

    [SerializeField] private float timerDuration;
    private float timer;

    private void Start()
    {
        timer = timerDuration;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ResetTimer();
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        // Choose enemy type
        int randEnemy = Random.Range(0, 2);
        // Choose spawner
        int randSpawner = Random.Range(0, spawners.Length);
        // Spawn enemy
        Instantiate(enemies[randEnemy], spawners[randSpawner].transform.position, Quaternion.identity);
    }

    public void ResetTimer()
    {
        timer = timerDuration;
    }
}
