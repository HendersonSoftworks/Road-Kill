using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private GameObject[] spawners;

    [SerializeField] private float timerDuration;
    private float timer;
    [SerializeField] public bool enemyActive;
    [SerializeField] GameObject currentEnemy;

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

        if (enemyActive == false)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            ResetTimer();
            currentEnemy = SpawnEnemy();
            enemyActive = true;
        }
    }

    public GameObject SpawnEnemy()
    {
        // Choose enemy type
        int randEnemy = Random.Range(0, enemies.Length);
        // Choose spawner
        int randSpawner = Random.Range(0, spawners.Length);
        // Spawn enemy
        var enemy = Instantiate(enemies[randEnemy], spawners[randSpawner].transform.position, Quaternion.identity);

        return enemy;
    }

    public void ResetTimer()
    {
        timer = timerDuration;
    }
}
