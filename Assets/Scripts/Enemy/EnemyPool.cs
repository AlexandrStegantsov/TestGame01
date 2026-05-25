using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private int initialSize = 10;

    private readonly Queue<GameObject>
        pool = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            Create();
        }
    }

    private GameObject Create()
    {
        GameObject enemy =
            Instantiate(
                enemyPrefab,
                transform);

        enemy.SetActive(false);

        EnemyAI enemyAI =
            enemy.GetComponent<EnemyAI>();

        if (enemyAI != null)
        {
            enemyAI.SetTarget(target);
        }

        pool.Enqueue(enemy);

        return enemy;
    }

    public GameObject GetEnemy(
        Vector3 position,
        Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            Create();
        }

        GameObject enemy =
            pool.Dequeue();

        enemy.transform.SetPositionAndRotation(
            position,
            rotation);

        enemy.SetActive(true);

        return enemy;
    }

    public void ReturnEnemy(
        GameObject enemy)
    {
        enemy.SetActive(false);

        pool.Enqueue(enemy);
    }
}