using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypesSo : ScriptableObject
{
    public string nameString;
    public GameObject prefab;
    public Sprite sprite;
    public ResourceGeneratorData resourceGeneratorData;
}
