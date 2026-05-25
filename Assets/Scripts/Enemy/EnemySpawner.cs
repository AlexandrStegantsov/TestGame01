using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyPool enemyPool;

    [SerializeField] private Transform[] spawnPoints;

    [Header("Settings")]
    [SerializeField] private float spawnInterval = 3f;

    [SerializeField] private float minSpawnInterval = 0.5f;

    [SerializeField] private float difficultyIncreaseRate = 0.1f;

    [SerializeField] private int maxEnemies = 10;

    private int currentEnemies;

    private void Start()
    {
        StartCoroutine(
            SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                spawnInterval);

            if (currentEnemies >= maxEnemies)
                continue;

            SpawnEnemy();

            spawnInterval =
                Mathf.Max(
                    minSpawnInterval,
                    spawnInterval -
                    difficultyIncreaseRate);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
            return;

        Transform spawnPoint =
            spawnPoints[
                Random.Range(
                    0,
                    spawnPoints.Length)];

        NavMeshHit hit;

        if (NavMesh.SamplePosition(
            spawnPoint.position,
            out hit,
            5f,
            NavMesh.AllAreas))
        {
            GameObject enemy =
                enemyPool.GetEnemy(
                    hit.position,
                    Quaternion.identity);

            currentEnemies++;

            Health health =
                enemy.GetComponent<Health>();

            if (health != null)
            {
                health.SetPool(enemyPool);

                health.OnDeath -=
                    HandleEnemyDeath;

                health.OnDeath +=
                    HandleEnemyDeath;
            }
        }
    }

    private void HandleEnemyDeath()
    {
        currentEnemies--;
    }
}