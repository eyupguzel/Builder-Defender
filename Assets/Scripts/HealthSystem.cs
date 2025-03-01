using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int healthMax;
    private int health;

    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    private void Awake()
    {
        health = healthMax;
    }
    public void SetHealthMax(BuildingTypesSo buildingType,bool updateHealth)
    {
        healthMax = buildingType.healthMax;

        if(updateHealth )
            health = healthMax;
    }
    public void Damage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, healthMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if(IsDead())
            OnDied?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return health == 0;
    }
    public int GetHealth()
    {
        return health;
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
