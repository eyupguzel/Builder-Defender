using UnityEngine;
using System.Collections.Generic;
using System;

public class SummaryManager : Singleton<SummaryManager>
{
    private Dictionary<ResourceTypeSO,int> collectedResources = new Dictionary<ResourceTypeSO,int>();
    public Dictionary<EnemiesSO,int> diededEnemy = new Dictionary<EnemiesSO,int>();

   // public event Action<ResourceTypeSO, int> OnResourceCollected;
    public void AddResource(ResourceTypeSO resourceType,int amount)
    {
        if (collectedResources.ContainsKey(resourceType))
        {
            collectedResources[resourceType] += amount;
        }
        else
            collectedResources[resourceType] = amount;

       // OnResourceCollected?.Invoke(resourceType, amount);
    }
    public void AddDiededEnemy(EnemiesSO enemyType, int amount)
    {
        if (diededEnemy.ContainsKey(enemyType))
        {
            diededEnemy[enemyType] += amount;
        }
        else
            diededEnemy[enemyType] = amount;
    }
    public Dictionary<EnemiesSO, int> GetAllDiededEnemies() => new Dictionary<EnemiesSO, int>(diededEnemy);
    public Dictionary<ResourceTypeSO, int> GetAllResources() => new Dictionary<ResourceTypeSO, int>(collectedResources);
}
