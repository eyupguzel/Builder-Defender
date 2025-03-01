using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypesSo buildingType;
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void Start()
    {
        healthSystem.SetHealthMax(buildingType,true);
    }
    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
