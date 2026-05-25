using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    [SerializeField] private GameObject vfxPrefab;

    [SerializeField] private int initialSize = 10;

    private readonly Queue<GameObject> pool = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            Create();
        }
    }

    private GameObject Create()
    {
        GameObject vfx =
            Instantiate(vfxPrefab, transform);

        vfx.SetActive(false);

        pool.Enqueue(vfx);

        return vfx;
    }

    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            Create();
        }

        GameObject vfx =
            pool.Dequeue();

        ParticleSystem ps =
            vfx.GetComponent<ParticleSystem>();

        ps.Stop(
            true,
            ParticleSystemStopBehavior
                .StopEmittingAndClear);

        vfx.SetActive(true);

        return vfx;
    }

    public void Return(GameObject vfx)
    {
        vfx.SetActive(false);

        pool.Enqueue(vfx);
    }
}