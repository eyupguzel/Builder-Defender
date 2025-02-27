using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager>
{
    private Cursor cursor;
    BuildingTypesSo buildingType;
    BoxCollider2D boxCollider;
    TooltipUI tooltip;
    private void Start()
    {
        cursor = Cursor.Instance;
        cursor.onClickGround += CreateBuildingObject;
        tooltip = TooltipUI.Instance;
    }
    public void SetActiveBuildingType(BuildingTypesSo buildingType)
    {
        this.buildingType = buildingType;
    }
    public BuildingTypesSo GetActiveBuildingType()
    {
        return buildingType;
    }
    private void CreateBuildingObject()
    {
        if (buildingType != null)
        {
            if (CanSpawnBuilding(buildingType, cursor.GetMouseWorldPosition(), out string errorMessage))
            {
                if (ResourceManager.Instance.CanAfford(buildingType.resourceAmount))
                {
                    ResourceManager.Instance.SpendResource(buildingType.resourceAmount);
                    Instantiate(buildingType.prefab, cursor.GetMouseWorldPosition(), Quaternion.identity);
                }
                else
                    tooltip.Show("Cannot afford !", new TooltipTimer { timer = 2f });
            }
            else
                tooltip.Show(errorMessage,new TooltipTimer { timer = 2f});
        }

    }
    private bool CanSpawnBuilding(BuildingTypesSo buildingType, Vector3 position, out string errorMesage)
    {
        boxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider.offset, boxCollider.size, 0);

        bool isAreaClear = collider2D.Length == 0;

        if (!isAreaClear)
        {
            errorMesage = "Area is not clear !";
            return false;
        }

        collider2D = Physics2D.OverlapCircleAll(transform.position, 10f);

        foreach (Collider2D collider in collider2D)
        {
            BuildingTypeHolder buildingTypeHolder = collider.gameObject.GetComponent<BuildingTypeHolder>();

            if (buildingTypeHolder != null)
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    errorMesage = "Too close to another building of the same type!";
                    return false;
                }
        }
        errorMesage = "";
        return true;
    }

}

