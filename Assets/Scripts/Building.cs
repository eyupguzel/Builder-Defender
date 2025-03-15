using System;
using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] BuildingData buildingData;
    [SerializeField] private bool upgradeable;

    public int CurrentLevel { get; protected set; }
    public int Health { get; protected set; }
    public int Damage { get; protected set; }

    private BuildingTypesSo buildingType;
    private BaseBuilding baseBuilding;
    private HealthSystem healthSystem;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        healthSystem = GetComponent<HealthSystem>();

        healthSystem = GetComponent<HealthSystem>();

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        baseBuilding = GetComponent<BaseBuilding>();
        healthSystem.OnDied += HealthSystem_OnDied;

        buildingDemolishBtn = transform.Find("BuildingDemolishBtn");
        buildingRepairBtn = transform.Find("BuildingRepairBtn");
    }
    private void Start()
    {
        if (upgradeable)
        {
            ApplyLevelData();
            healthSystem.SetHealthMax(Health);
        }
        else
            healthSystem.SetHealthMax(buildingType.healthMax);


        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHeal += HealthSystem_OnHeal;
        if (buildingDemolishBtn != null)
         buildingDemolishBtn.gameObject.SetActive(false);

        HideRepairButton();
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
    }

    private void HealthSystem_OnHeal()
    {
        if (healthSystem.IsHealthFull())
            HideRepairButton();
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        ShowRepairButton();
    }

    private void HealthSystem_OnDied()
    {
        Destroy(gameObject);
    }
    private void OnMouseEnter()
    {
        ShowDemolishButton();
    }
    private void OnMouseExit()
    {
        HideDemolishButton();
    }

    private void ShowDemolishButton()
    {
        if (buildingDemolishBtn != null)
            buildingDemolishBtn.gameObject.SetActive(true);
    }
    private void HideDemolishButton()
    {
        if (buildingDemolishBtn != null)
            StartCoroutine(HideDemolishButtonTimer());
    }
    private void ShowRepairButton()
    {
        if (buildingRepairBtn != null)
            buildingRepairBtn.gameObject.SetActive(true);
    }
    private void HideRepairButton()
    {
        if (buildingRepairBtn != null)
            buildingRepairBtn.gameObject.SetActive(false);
    }
    private IEnumerator HideDemolishButtonTimer()
    {
        yield return new WaitForSeconds(4f);
        buildingDemolishBtn.gameObject.SetActive(false);
    }

}
