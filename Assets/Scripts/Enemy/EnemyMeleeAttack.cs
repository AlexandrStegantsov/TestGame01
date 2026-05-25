using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float damage = 10f;

    [SerializeField] private float attackCooldown = 1f;

    private float nextAttackTime;

    private IDamageable targetDamageable;

    public void SetTarget(
        Transform target)
    {
        targetDamageable =
            target.GetComponent<IDamageable>();

        if (targetDamageable == null)
        {
            targetDamageable =
                target.GetComponentInParent<IDamageable>();
        }
    }

    public void TryAttack()
    {
        if (targetDamageable == null)
            return;

        if (Time.time < nextAttackTime)
            return;

        nextAttackTime =
            Time.time + attackCooldown;

        targetDamageable.TakeDamage(
            damage);

        Debug.LogError(
            $"Enemy attacked player for {damage}");
    }
}