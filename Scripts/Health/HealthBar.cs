using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;

    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        UpdateHealthBar();
    }

    private void OnEnable()
    {
        health.OnTakeDamage += HandleTakeDamage;
        health.OnHeal += HandleHeal;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= HandleTakeDamage;
        health.OnHeal -= HandleHeal;
    }

    private void HandleHeal(int currentHealth)
    {
        UpdateHealthBar();
    }

    private void HandleTakeDamage(int damage, int currentHealth)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthBar.value = (float)health.CurrentHealth / health.MaxHealth;
    }
}
