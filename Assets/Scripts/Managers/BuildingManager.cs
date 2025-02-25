using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager>
{
    private Cursor cursor;
    BuildingTypesSo buildingType;
    BoxCollider2D boxCollider;
    private void Start()
    {
        cursor = Cursor.Instance;
        cursor.onClickGround += CreateBuildingObject;
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
        if (buildingType != null && CanSpawnBuilding(buildingType, cursor.GetMouseWorldPosition()))
            Instantiate(buildingType.prefab, cursor.transform.position, Quaternion.identity);
    }
    private bool CanSpawnBuilding(BuildingTypesSo buildingType, Vector3 position)
    {
        boxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider.offset, boxCollider.size, 0);

        bool isAreaClear = collider2D.Length == 0;

        if (!isAreaClear)
            return false;

        collider2D = Physics2D.OverlapCircleAll(transform.position, 10f);

        foreach (Collider2D collider in collider2D)
        {
            BuildingTypeHolder buildingTypeHolder = collider.gameObject.GetComponent<BuildingTypeHolder>();

            if (buildingTypeHolder != null)
                if (buildingTypeHolder.buildingType == buildingType)
                    return false;
        }

        return true;
    }
}
