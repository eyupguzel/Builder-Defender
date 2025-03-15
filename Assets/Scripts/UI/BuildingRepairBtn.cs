using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairhBtn : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    private void Start()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            healthSystem.HealFull();
            int missingHealth = healthSystem.GetHealthMax() - healthSystem.GetHealth();
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[] { new ResourceAmount { resourceType = goldResourceType, cost = repairCost } };

            if (ResourceManager.Instance.CanAfford(resourceAmountCost))
            {
                ResourceManager.Instance.SpendResource(resourceAmountCost);
                healthSystem.HealFull();
            }
            else
                TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipTimer { timer = 2f });

        });
    }
}
