using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public Sprite sprite;
    public string nameString;
    public string nameShort;
    public string colorHex;
}
