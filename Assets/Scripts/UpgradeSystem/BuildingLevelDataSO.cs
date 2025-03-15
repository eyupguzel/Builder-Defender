using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingLevelData")]
public class BuildingLevelDataSO : ScriptableObject
{
    public int level;
    public int health;
    public int damage;
    public Sprite sprite;
    public ResourceAmount[] resourceAmount;
}
