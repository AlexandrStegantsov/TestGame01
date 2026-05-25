using System;
using UnityEngine;

public class Health :
    MonoBehaviour,
    IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private float currentHealth;

    private EnemyPool ownerPool;

    public float CurrentHealth =>
        currentHealth;

    public float MaxHealth =>
        maxHealth;

    public event Action<float, float>
        OnHealthChanged;

    public event Action OnDeath;

    private void OnEnable()
    {
        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(
            currentHealth,
            maxHealth);
    }

    public void SetPool(
        EnemyPool pool)
    {
        ownerPool = pool;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth =
            Mathf.Clamp(
                currentHealth,
                0f,
                maxHealth);

        OnHealthChanged?.Invoke(
            currentHealth,
            maxHealth);

        Debug.Log(
            $"{name} took {damage} damage");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        IAchievementService achievementService =
            ServiceLocator.Get<IAchievementService>();

        achievementService.RegisterKill();

        OnDeath?.Invoke();

        if (ownerPool != null)
        {
            ownerPool.ReturnEnemy(
                gameObject);
        }
    }
}