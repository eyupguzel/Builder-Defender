using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypesSo : ScriptableObject
{
    public string nameString;
    public GameObject prefab;
    public Sprite sprite;
    public ResourceGeneratorData resourceGeneratorData;
    public ResourceAmount[] resourceAmount;

    public string GetConstructionResourceCostString()
    {
        string str = "";
        foreach (ResourceAmount resource in resourceAmount)
        {
            str += "<color=#" + resource.resourceType.colorHex + "> " + resource.resourceType.nameShort + resource.cost + "</color> "; 
        }
        return str;
    }
}
