using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth = 100;

    public int health;

    public Action<int, int> OnTakeDamage;
    public Action<int> OnHeal;
    public Action OnDie;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => health;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
        OnHeal?.Invoke(health);
    }

    public void Heal(int amount)
    {
        if (IsDead) { return; }

        health = Mathf.Min(health + amount, maxHealth);

        OnHeal?.Invoke(health);
    }

    public void Damage(int amount)
    {
        if (IsDead) { return; }

        health = Mathf.Max(health - amount, 0);

        OnTakeDamage?.Invoke(amount, health);

        if (IsDead)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
