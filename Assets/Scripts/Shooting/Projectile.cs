using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  
    public event Action<Vector3> OnHit;
    
    private float speed;

    private Rigidbody rb;

    private ProjectilePool ownerPool;

    private Vector3 lastPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawLine(
            lastPosition,
            transform.position,
            Color.red);

        lastPosition = transform.position;
    }

    public void Initialize(
        float projectileSpeed,
        ProjectilePool pool)
    {
        speed = projectileSpeed;

        ownerPool = pool;

        rb.linearVelocity =
            transform.forward * speed;

        lastPosition =
            transform.position;

        Invoke(nameof(ReturnToPool), 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(
            $"Hit to {other.name}");
        
        
        IDamageable damageable =
            other.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(10f);
        }
        else
        {
            Debug.LogError("No IDamageable found");
        }
        OnHit?.Invoke(transform.position);
        OnHit = null;

        ReturnToPool();
    }

   
   

    private void ReturnToPool()
    {
        CancelInvoke();

        rb.linearVelocity =
            Vector3.zero;

        ownerPool.ReturnProjectile(this);
    }
}