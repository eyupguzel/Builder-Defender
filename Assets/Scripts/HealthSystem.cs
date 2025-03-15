using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int healthMax;
    private int health;

    public event EventHandler OnDamaged;
    public event Action OnHeal;
    public event Action OnDied;

    private bool oneTime = true;
    public void SetHealthMax(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }
    public void SetHealthMax(EnemiesSO enemyType, bool updateHealth)
    {
        healthMax = enemyType.healthMax;

        if (updateHealth)
            health = healthMax;
    }
    public void Damage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, healthMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead() && oneTime)
        {
            OnDied?.Invoke();
            oneTime = false;
        }
    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, healthMax);

        OnHeal?.Invoke();
    }
    public void HealFull()
    {
        healthMax = health;
        OnHeal?.Invoke();
    }

    public bool IsDead()
    {
        return health <= 0;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetHealthMax()
    {
        return healthMax;
    }
    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }
    public bool IsHealthFull()
    {
        return health == healthMax;
    }
}
