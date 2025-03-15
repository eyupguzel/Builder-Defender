using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => { Destroy(building.gameObject); });
       // BuildingTypesSo buildingTypeSo = building.GetComponent<BuildingTypesSo>();
    }
}
