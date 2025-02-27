using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform bar;

    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        bar = transform.Find("bar").GetComponent<Transform>();

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
        bar.localScale = new Vector3(1 - resourceGenerator.GetResourceTimeNormalized(), 1, 1);
    }
    private void Update()
    {
        bar.localScale = new Vector3(1-resourceGenerator.GetResourceTimeNormalized(), 1, 1);
    }
}
