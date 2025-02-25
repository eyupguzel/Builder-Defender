using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    public  Action OnResourceAmountUpdate;

    protected override void Init()
    {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>("ResourcesTypeListSO");

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }
    }
    public void AddResource(ResourceTypeSO type,int amount)
    {
        resourceAmountDictionary[type] += amount;
        OnResourceAmountUpdate?.Invoke();
    }
    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}
