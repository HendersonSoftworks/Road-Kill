using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private GameObject[] spawners;

    [SerializeField] private float timerDuration;
    private float timer;

    [SerializeField] DeathManager deathManager;

    private void Start()
    {
        timer = timerDuration;

        deathManager = FindObjectOfType<DeathManager>();

    }

    private void Update()
    {
        // Return if character is dead
        if (deathManager.isDead)
        {
            return;
        }


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
        int randEnemy = Random.Range(0, enemies.Length);
        // Choose spawner
        int randSpawner = Random.Range(0, spawners.Length);
        // Spawn enemy
        Instantiate(enemies[randEnemy], spawners[randSpawner].transform.position, Quaternion.identity);
        //Instantiate(enemies[randEnemy]);

    }

    public void ResetTimer()
    {
        timer = timerDuration;
    }
}
