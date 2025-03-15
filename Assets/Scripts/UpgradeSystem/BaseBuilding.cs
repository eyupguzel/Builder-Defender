using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    /*[SerializeField] BuildingData buildingData;
    private HealthSystem healthSystem;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        healthSystem = GetComponent<HealthSystem>();
        ApplyLevelData();
    }
    public void Upgrade()
    {
        if (CurrentLevel < buildingData.buildingLevelDataSO.Length - 1)
        {
            BuildingLevelDataSO nextLevel = buildingData.buildingLevelDataSO[CurrentLevel + 1];

            if (ResourceManager.Instance.CanAfford(nextLevel.resourceAmount))
            {
                ResourceManager.Instance.SpendResource(nextLevel.resourceAmount);
                CurrentLevel++;
                ApplyLevelData();
                healthSystem.SetHealthMax(Health);
                healthSystem.HealFull();
            }
        }
    }
    private void ApplyLevelData()
    {
        BuildingLevelDataSO data = buildingData.buildingLevelDataSO[CurrentLevel];
        Damage = data.damage;
        Health = data.health;
        spriteRenderer.sprite = data.sprite;

        UpdateColliderSize();
    }
    private void UpdateColliderSize()
    {
        boxCollider.size = spriteRenderer.bounds.size;
        boxCollider.offset = Vector2.zero;
    }*/
}
