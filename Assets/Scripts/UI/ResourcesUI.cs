using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : Singleton<ResourcesUI>
{
    [SerializeField] private float offset;

    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeDictionary;
    void Start()
    {
        ResourceManager.Instance.OnResourceAmountUpdate += UpdateResourceAmount;
        resourceTypeList = Resources.Load<ResourceTypeListSO>("ResourcesTypeListSO");
        resourceTypeDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypeDictionary[resourceType] = resourceTransform;

            index++;
        }

        UpdateResourceAmount();
    }
    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTypeDictionary[resourceType];

            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance.GetResourceAmount(resourceType).ToString();
        }
    }
}
