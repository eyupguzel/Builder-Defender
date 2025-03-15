using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    private float constructionTimer;
    private float constructionTimerMax;

    private BuildingTypesSo buildingType;
    private BuildingTypeHolder buildingTypeHolder;

    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
    }
    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        if (constructionTimer <= 0)
        {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public static BuildingConstruction CreateBuildingConstruction(Vector3 position,BuildingTypesSo buildingType)
    {
        Transform prefab = Resources.Load <Transform>("BuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(prefab,position,Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.Setup(buildingType.constructionTimerMax,buildingType);
        return buildingConstruction;
    }

    private void Setup(float constructionTimerMax,BuildingTypesSo buildingType)
    {
        this.constructionTimerMax = constructionTimerMax;
        this.buildingType = buildingType;
        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType.sprite;

        buildingTypeHolder.buildingType = buildingType;

        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
    }
    public float GetBuildingConstructionTimerNormalized()
    {
        return constructionTimer / constructionTimerMax;
    }
}
