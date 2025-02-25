using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    private Transform arrowBtn;
    private Dictionary<BuildingTypesSo, Transform> buildingTypeDictionary;
    [SerializeField] private List<BuildingTypesSo> ignoreBuildingTypeList;
    private float offset = 160;
    private int index;
    private void Start()
    {
        buildingTypeDictionary = new Dictionary<BuildingTypesSo, Transform>();
        Transform buildingTypeTemplete = transform.Find("buildingTypeTemplate");
        buildingTypeTemplete.gameObject.SetActive(false);

        //Cursor.Instance.onClickUI += UpdateSelectedButton;

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>("BuildingTypeList");

        arrowBtn = Instantiate(buildingTypeTemplete, transform);
        arrowBtn.gameObject.SetActive(true);

        offset = 130;

        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);
        arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;

        arrowBtn.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(null); });
        index++;


        foreach (BuildingTypesSo buildingType in buildingTypeList.buildingList)
        {
            if (ignoreBuildingTypeList.Contains(buildingType))
                continue;
            Transform btnTransform = Instantiate(buildingTypeTemplete, transform);
            btnTransform.gameObject.SetActive(true);

            offset = 130;

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index,0);
            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(buildingType); });
            buildingTypeDictionary[buildingType] = btnTransform;
            index++;
        }
        SetSelectedUI();
    }
    private void Update()
    {
        UpdateSelectedButton();
    }
    private void UpdateSelectedButton()
    {
        SetSelectedUI();
        BuildingTypesSo activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if (activeBuildingType != null)
        {
            Cursor.Instance.SetCursorSprite(activeBuildingType);
            buildingTypeDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
        else
        {
            Cursor.Instance.SetCursorSprite(null);
            arrowBtn.Find("selected").gameObject.SetActive(true);
        }

    }

    private void SetSelectedUI()
    {
        arrowBtn.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypesSo buildingType in buildingTypeDictionary.Keys)
        {
            Transform btnTransform = buildingTypeDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }
    }
}
