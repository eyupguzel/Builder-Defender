using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgradeBtn : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Awake()
    {
        transform.Find("upgradeBtn").GetComponent<Button>().onClick.AddListener(() => { building.Upgrade(); });
    }
}
