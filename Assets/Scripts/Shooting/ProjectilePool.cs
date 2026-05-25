using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int initialSize = 20;

    private readonly Queue<Projectile> pool = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateProjectile();
        }
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile =
            Instantiate(
                projectilePrefab,
                transform);

        projectile.gameObject.SetActive(false);

        pool.Enqueue(projectile);

        return projectile;
    }

    public Projectile GetProjectile()
    {
        if (pool.Count == 0)
        {
            CreateProjectile();
        }

        Projectile projectile =
            pool.Dequeue();

        projectile.gameObject.SetActive(true);

        return projectile;
    }

    public void ReturnProjectile(
        Projectile projectile)
    {
        projectile.gameObject.SetActive(false);

        pool.Enqueue(projectile);
    }
}